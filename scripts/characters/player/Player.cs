using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private float acceleration = 50;
	private float friction = 25f;
	private Node2D ripples;

	private AnimatedSprite2D animation;
	private AnimatedSprite2D oreAnimation;

	private float rowForce = 5f;
	private float maxRowSpeed = 5;
	private Vector2 velocity = Vector2.Zero;

	public override void _Ready()
	{
		oreAnimation = GetNode<AnimatedSprite2D>("Player/GFX/OreAnimation");
		animation = GetNode<AnimatedSprite2D>("Player/GFX/PlayerAnimation");
		ripples = GetNode<Node2D>("Player/GFX/Ripples");

	}

	public override void _PhysicsProcess(double delta)
	{
		
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down").Normalized();

		if(direction != Vector2.Zero)
		{
			animation.Play("Row");
			oreAnimation.Play("Row");
		}
		else
		{
			oreAnimation.Stop();
			animation.Stop();
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
}



