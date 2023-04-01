using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private bool IsOnLand = false;
	private bool IsRowing = false;
	private float acceleration = 50;
	private float friction = 25f;
	private Node2D ripples;
	private Node2D landAnimations;

	private AnimatedSprite2D animation;
	private AnimatedSprite2D moveUpAnimation;
	private AnimatedSprite2D moveDownAnimation;
	private AnimatedSprite2D moveLeftAnimation;

	private AnimatedSprite2D moveRightAnimation;

	private AnimatedSprite2D oreAnimation;
	private AudioStreamPlayer2D rowSFX;

	private float rowForce = 5f;
	private float maxRowSpeed = 5;
	private Vector2 velocity = Vector2.Zero;




	public override void _Ready()
    {

		InitMovementAnimations();

        oreAnimation = GetNode<AnimatedSprite2D>("Player/GFX/Animations/OreAnimation");
        animation = GetNode<AnimatedSprite2D>("Player/GFX/Animations/PlayerAnimation");   
		landAnimations =    GetNode<Node2D>("Player/GFX/Animations/LandAnimations");   
        ripples = GetNode<Node2D>("Player/GFX/Ripples");
        rowSFX = GetNode<AudioStreamPlayer2D>("Player/SFX/Row");

    }

    public override void _Process(double delda)
	{

	}


    public override void _PhysicsProcess(double delta)
	{
				
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down").Normalized();

		if(IsOnLand)
		{
			rowSFX.Stop();
			oreAnimation.Visible = false;
			animation.Visible = false;
			landAnimations.Visible = true;
			MoveAnimations(direction);
		}
		else
		{
			oreAnimation.Visible = true;
			animation.Visible = true;
			landAnimations.Visible = false;
		}

		if(IsRowing)
		{
			animation.Play("Row");
			oreAnimation.Play("Row");
			if(rowSFX.Playing != true)
			{
				rowSFX.Play();
			}
		}
		else
		{
			if(rowSFX.Playing == true)
			{
				rowSFX.Stop();
			}

			animation.Stop();
			oreAnimation.Stop();
		}


		if(direction != Vector2.Zero)
		{
			if(!IsOnLand)
			{
				IsRowing = true;
			}

		}
		else
		{

			if(!IsOnLand)
			{
				IsRowing = false;
			}

		}

		velocity += direction * acceleration * (float)delta;

		float frictionForce = friction * (float)delta;

		if(velocity.Length() > frictionForce)
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

		if(collision != null)
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


	private void InitMovementAnimations()
    {
        moveUpAnimation = GetNode<AnimatedSprite2D>("Player/GFX/Animations/LandAnimations/MoveUp");
        moveDownAnimation = GetNode<AnimatedSprite2D>("Player/GFX/Animations/LandAnimations/MoveDown");
        moveLeftAnimation = GetNode<AnimatedSprite2D>("Player/GFX/Animations/LandAnimations/MoveLeft");
        moveRightAnimation = GetNode<AnimatedSprite2D>("Player/GFX/Animations/LandAnimations/MoveRight");
    }

	private void ResetMoveAnimations(AnimatedSprite2D exclude)
	{
	    foreach (var anim in new[] { moveUpAnimation, moveDownAnimation, moveLeftAnimation, moveRightAnimation })
	    {
	        if (anim != exclude)
	        {
	            anim.Visible = false;
	            anim.Stop();
	        }
	    }
	}

	private void MoveAnimations(Vector2 direction)
	{
	    if (direction == Vector2.Up)
	    {
	        moveUpAnimation.Visible = true;
	        moveUpAnimation.Play();
	        ResetMoveAnimations(moveUpAnimation);
	    }
	    else if (direction == Vector2.Down)
	    {
	        moveDownAnimation.Visible = true;
	        moveDownAnimation.Play();
	        ResetMoveAnimations(moveDownAnimation);
	    }
	    else if (direction == Vector2.Left)
	    {
	        moveLeftAnimation.Visible = true;
	        moveLeftAnimation.Play();
	        ResetMoveAnimations(moveLeftAnimation);
	    }
	    else if (direction == Vector2.Right)
	    {
	        moveRightAnimation.Visible = true;
	        moveRightAnimation.Play();
	        ResetMoveAnimations(moveRightAnimation);
	    }
	}


	public void _on_land_check_area_entered(Area2D area)
	{
		if(area.IsInGroup("LAND"))
		{
			GD.Print("ON LAND");
			IsOnLand = true;
		}
	}


	public void _on_land_check_area_exited(Area2D area)
	{
		if(area.IsInGroup("LAND"))
		{
			GD.Print("OFF LAND");
			IsOnLand = false;
		}
	}
}



