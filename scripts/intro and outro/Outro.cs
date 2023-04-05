using Godot;
using System;

public partial class Outro : Node2D
{
	public delegate AnimationPlayer AnimationPlayerDelegate();
	public delegate AnimatedSprite2D AnimatedSprite2DDelegate();

	// Called when the node enters the scene tree for the first time.
	[Export]
	public AnimatedSprite2D Sprite;
    [Export]
    public PackedScene StartScene;

	[Export]
	public AnimationPlayer AnimationPlayer;

	[Export]
	public AudioStreamPlayer Birds;


	[Export]
	public Sprite2D End;

	[Export]
	public Label Text1;
	[Export]
	public Label Text2;
	[Export]
	public Label Text3;
	[Export]
	public Label Text4;

    public override void _Ready()
	{
		Sprite.Play("1");
		AnimationPlayer.Play("Text1");
		Text1.Visible = true;
		End.Visible = false;

		AnimationPlayer.AnimationFinished += OnAnimationFinished;
		Sprite.AnimationFinished += OnSpriteAnimationFinished;

	}


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{

    }

    private void OnAnimationFinished(StringName animName)
    {
        GD.Print("ANIMATION FINISHED: " + animName);
		if (animName == "Text1")
		{
			Text1.Visible = false;
			Text2.Visible = true;
			Sprite.Play("2");
			AnimationPlayer.Play("Text2");
		}
		else if (animName == "Text2")
		{
			Text2.Visible = false;
			Text3.Visible = true;
			Sprite.Play("2.5");
			AnimationPlayer.Play("Text3");
		}
		else if (animName == "Text3")
		{
			Text3.Visible = true;
			Sprite.Play("3");
		}
		else if (animName == "Text3")
		{

			Text3.Visible = false;
			Text4.Visible = true;
			Sprite.Play("4");
			AnimationPlayer.Play("Text4");
		}
		else if(animName == "Text4")
		{
			Birds.Play();
			End.Visible = true;
			Text4.Visible = false;

			AnimationPlayer.Play("End");
			Sprite.Play("5");
		}

    }

	 private void OnSpriteAnimationFinished()
    {
		GD.Print("SPRITE ANIMATION FINISHED");      
			Text3.Visible = false;
			Sprite.Play("4");
			AnimationPlayer.Play("Text4");
    }

}
