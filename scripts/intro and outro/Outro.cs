using Godot;
using System;

public partial class Outro : Node2D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public AnimatedSprite2D Sprite;
    [Export]
    public PackedScene StartScene;
    public override void _Ready()
	{
		Sprite.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!Sprite.IsPlaying())
		{
            GetTree().ChangeSceneToFile(StartScene.ResourcePath);

        }
    }
}
