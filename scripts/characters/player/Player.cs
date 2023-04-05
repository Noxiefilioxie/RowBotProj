using Godot;
using RowBot.scripts.inventory;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Player : CharacterBody2D
{

    public int items = 0;
	private bool previousIsOnLand = false;
	private bool IsOnLand = false;
	private bool IsRowing = false;
	private float acceleration = 50;
	private float friction = 25f;

	private float Speed = 200f;

	private Node2D ripples;
	private Node2D landGFX;
	private Node2D boatGFX;

	private AnimationTree animationTree;

	private AnimatedSprite2D animation;



	private AudioStreamPlayer2D rowSFX;
	private AudioStreamPlayer2D threadsSFX;
	private AudioStreamPlayer2D pickupItemSFX;

    private AudioStreamPlayer2D WaveAudio;


	private float rowForce = 10f;
	private float maxRowSpeed = 5;
	private Vector2 velocity = Vector2.Zero;

    private Node2D Compass;




	public override void _Ready()
    {
		animationTree = GetNode<AnimationTree>("AnimationTree");

        animation = GetNode<AnimatedSprite2D>("Player/BoatGFX/Animations/PlayerAnimation");   
		landGFX = GetNode<Node2D>("Player/LandGFX");
		boatGFX = GetNode<Node2D>("Player/BoatGFX"); 

        ripples = GetNode<Node2D>("Player/BoatGFX/Ripples");
        rowSFX = GetNode<AudioStreamPlayer2D>("Player/SFX/Row");
		threadsSFX = GetNode<AudioStreamPlayer2D>("Player/SFX/ThreadsMove");
		pickupItemSFX = GetNode<AudioStreamPlayer2D>("Player/SFX/Pickup");
        WaveAudio = GetNode<AudioStreamPlayer2D>("Player/SFX/WaveAudio");


        Compass = GetNode<Node2D>("UI/Compass/CompassNeedle");
    }

    public override void _Process(double delta)
	{

        var islandItem = GetTree().GetNodesInGroup("ISLANDITEM");

        var island = GetTree().GetNodesInGroup("MAINISLAND");

        var islandmain = (Node2D)island.FirstOrDefault();

        var itemsLeft = new List<Node2D>();

        foreach (var item in islandItem)
        {
            itemsLeft.Add((Node2D)item);
        }
        if(itemsLeft.Count == 0)
        {
            var angle = GlobalPosition.AngleToPoint(islandmain.GlobalPosition);
            Compass.GlobalRotation = angle;
            GD.Print("No items left");
        }
        else
        {
            var closestItem = itemsLeft.OrderBy(x => x.GlobalPosition.DistanceTo(GlobalPosition)).First();
            var angle = GlobalPosition.AngleToPoint(closestItem.GlobalPosition);
            Compass.GlobalRotation = angle;
        }


	}


    public override void _PhysicsProcess(double delta)
    {

        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down").Normalized();

        if (IsOnLand)
        {
            rowSFX.Stop();
			boatGFX.Visible = false;;

            animation.Visible = false;
            landGFX.Visible = true;
            MoveAnimations(direction);
        }
        else
        {
			boatGFX.Visible = true;
            animation.Visible = true;
            landGFX.Visible = false;
        }

        if (IsRowing)
        {
            animation.Play("Row");
            if (rowSFX.Playing != true)
            {
                rowSFX.Play();
            }
        }
        else
        {
            if (rowSFX.Playing == true)
            {
                rowSFX.Stop();
            }

            animation.Stop();
        }


        if (direction != Vector2.Zero)
        {
            if (!IsOnLand)
            {
				threadsSFX.Stop();
                IsRowing = true;
            }

			if(IsOnLand)
			{
				if(!threadsSFX.Playing)
				{
					threadsSFX.Play();
				}
			}

        }
        else
        {

			if(IsOnLand)
			{
				if(threadsSFX.Playing)
				{
					threadsSFX.Stop();
				}
			}

            if (!IsOnLand)
            {
                IsRowing = false;
            }

        }


        if (!IsOnLand)
        {
  	        BoatMovement(delta, direction);
            WaveAudio.VolumeDb = -20;
			previousIsOnLand = false;
        }
        else
        {
			if (!previousIsOnLand)
            {
                velocity = Vector2.Zero; // Reset the boat's velocity
                previousIsOnLand = true;
            }
             WaveAudio.VolumeDb = -30;
            LandMovement(delta, direction);
        }
    }

    private void LandMovement(double delta, Vector2 direction)
    {
		Rotation = 0f;
        Vector2 velocity = Velocity;

		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
    }

    private void BoatMovement(double delta, Vector2 direction)
    {
        velocity += direction * acceleration * (float)delta;
	

        float frictionForce = friction * (float)delta;

        if (velocity.Length() > frictionForce)
        {
            velocity -= velocity.Normalized() * frictionForce;
            ripples.Visible = false;
        }
        else
        {
            ripples.Visible = true;

            velocity = Vector2.Zero;
        }

        KinematicCollision2D collision = MoveAndCollide(velocity * (float)delta);

        if (collision != null)
        {
            velocity = velocity.Slide(collision.GetNormal());
        }

        LookAtDirection(velocity);

    }

    private void LookAtDirection(Vector2 direction)
    {
        if (direction.Length() > 0)
        {
            // Calculate the angle in radians between the direction vector and the positive X-axis
            float angle = Mathf.Atan2(direction.Y, direction.X);

            // Set the rotation of the character to face the movement direction
            Rotation = angle;
        }
    }


	private void MoveAnimations(Vector2 direction)
	{

		animationTree.Set("parameters/blend_position", direction);
	}


	public void _on_land_check_area_entered(Area2D area)
	{
		if(area.IsInGroup("LAND"))
		{
			GD.Print("ON LAND");
        
			IsOnLand = true;
			IsRowing = false;
		}

		if (area.IsInGroup("ITEM"))
		{

            var item = GetTree().Root.GetNode<Node2D>($"Main/UserInterface/Inventory/{area.GetParent().Name}");
			item.ZIndex = 0;
            area.GetParent().QueueFree();
            items++;
			pickupItemSFX.Play();

		}
        if (area.IsInGroup("FINISH"))
		{

            GetTree().ChangeSceneToFile("res://scenes/Outro.tscn");

		}
	}


	public void _on_land_check_area_exited(Area2D area)
	{
		if(area.IsInGroup("LAND"))
		{
			GD.Print("OFF LAND");
			IsOnLand = false;
			IsRowing = true;
		}
	}
}



