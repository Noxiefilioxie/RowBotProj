using Godot;
using System;

public partial class Player : CharacterBody2D
{


	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down").Normalized();




		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
		LookAtDirection(Velocity);
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



