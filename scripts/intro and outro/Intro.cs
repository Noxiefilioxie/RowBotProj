using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Intro : Node2D
{

    [Export]
    public AnimatedSprite2D Sprite;
    [Export]
    public PackedScene MainScene;
    [Export]
    public Label StoryText;
    public const float chunks = 0.16f;
    public const int numberOfAnimations = 6;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        Sprite.Play("1");

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        //for (int i = 1; i < numberOfAnimations; i++)
        //{
        //    //GD.Print("IN loop");
        //    //GD.Print(i);

        //    //for(float f = 0f; f < chunks; f += 0.01f)
        //    //{
        //    //	StoryText.VisibleRatio += f;
        //    //	GD.Print(StoryText.VisibleRatio);
        //    //}
        //    Sprite.Play($"{i}");

        //}
        if (!Sprite.IsPlaying())
        {
            GetTree().ChangeSceneToFile(MainScene.ResourcePath);

        }

      
    }

    public void _on_animated_sprite_2d_animation_finished()
    {
        GD.Print("FInished");
    }

}
