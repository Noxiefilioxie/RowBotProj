using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private float acceleration = 500f;
	private float friction = 200f;

	private AnimatedSprite2D animation;
	private float rowForce = 200f;
	private float maxRowSpeed = 100f;
	private Vector2 velocity = Vector2.Zero;

	public override void _Ready()
	{
		animation = GetNode<AnimatedSprite2D>("Player/GFX/PlayerAnimations");
	}

	public override void _PhysicsProcess(double delta)
	{
		
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down").Normalized();

		if(direction != Vector2.Zero)
		{
			animation.Play("Row");
		}
		else
		{
			animation.Stop();
		}


		velocity += direction * acceleration * (float)delta;

		float frictionForce = friction * (float)delta;

		if(velocity.Length() > frictionForce)
		{
			velocity -= velocity.Normalized() * frictionForce;
		}
		else
		{
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



