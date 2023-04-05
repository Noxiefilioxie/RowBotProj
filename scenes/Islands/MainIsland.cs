using Godot;
using System;

public partial class MainIsland : Node2D
{
	private Area2D Finish;

	private Player Player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Finish = GetNode<Area2D>("Finish");
		Player = GetTree().Root.GetNode<Player>("Main/Player");


		Finish.Visible = false;
		Finish.GetChild<CollisionShape2D>(0).Disabled = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Player.items == 5)
		{
			Finish.GetChild<CollisionShape2D>(0).Disabled = false;
			Finish.Visible = true;
		}

	}
}
