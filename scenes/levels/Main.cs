using Godot;
using System;

public partial class Main : Node2D
{
	[Export]
	public PackedScene[] Islands;
	[Export]
	public int MapSize = 100;
	public int ObjectSize = 60;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (Islands == null)
        {
            GD.PrintErr("No object scene assigned");
            return;
        }

        PlaceObjects();
	}

    private void PlaceObjects()
    {
         Random random = new Random();

        for (int i = 0; i < Islands.Length; i++)
        {
           int randomX = random.Next(0, MapSize);
            int randomY = random.Next(0, MapSize);

            Vector2 randomPosition = new Vector2(randomX * ObjectSize, randomY * ObjectSize);
            Node2D newObject = (Node2D)Islands[i].Instantiate();
            newObject.Position = randomPosition;
            AddChild(newObject);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
