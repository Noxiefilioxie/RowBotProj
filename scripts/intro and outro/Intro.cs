using Godot;
using System;

public partial class Intro : Node2D
{
	public delegate AnimationPlayer AnimationPlayerDelegate();

	[Export]
	public AnimatedSprite2D Sprite;
	[Export]
	public PackedScene MainScene;
	[Export]
	public AnimationPlayer AnimationPlayer;
	[Export]
	public Label Text1;
	[Export]
	public Label Text2;
	[Export]
	public Label Text3;
	[Export]
	public Label Text4;
	[Export]
	public Label Text5;
	[Export]
	public Label Text6;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{




		Sprite.Play("1");
		AnimationPlayer.Play("Text1");

		AnimationPlayer.AnimationFinished += OnAnimationFinished;



	}


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{

		// if (!Sprite.IsPlaying())
		// {
		// 	GetTree().ChangeSceneToFile(MainScene.ResourcePath);

		// }
	}


    private void OnAnimationFinished(StringName animName)
    {
        GD.Print("ANIMATION FINISHED: " + animName);
		if (animName == "Text1")
		{
			Text1.Visible = false;
			Sprite.Play("2");
			AnimationPlayer.Play("Text2");
		}
		else if (animName == "Text2")
		{
			Text2.Visible = false;
			
			Sprite.Play("3");
			AnimationPlayer.Play("Text3");
		}
		else if (animName == "Text3")
		{
			Text3.Visible = false;
			Sprite.Play("4");
			AnimationPlayer.Play("Text4");
		}
		else if (animName == "Text4")
		{
			Text4.Visible = false;
			Sprite.Play("5");
			AnimationPlayer.Play("Text5");
		}
		else if (animName == "Text5")
		{
			Text5.Visible = false;
			Sprite.Play("6");
			AnimationPlayer.Play("Text6");
		}
		else if (animName == "Text6")
		{			
			GetTree().ChangeSceneToFile(MainScene.ResourcePath);
		}

    }



}
