using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node2D
{
    [Export]
    public PackedScene[] Islands;
    [Export]
    public int MapSize = 100;
    public int ObjectSize = 60;

    private HashSet<Vector2> occupiedTiles = new HashSet<Vector2>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (Islands == null || Islands.Length == 0)
        {
            GD.PrintErr("No object scene assigned");
            return;
        }

        PlaceObjects();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }




    private void PlaceObjects()
    {
        Random random = new Random();

        for (int i = 0; i < Islands.Length; i++)
        {
            Vector2 randomTile;

            // Generate random tile coordinates until an unoccupied tile is found.
            do
            {
                int randomX = random.Next(0, MapSize);
                int randomY = random.Next(0, MapSize);
                randomTile = new Vector2(randomX, randomY);
            } while (occupiedTiles.Contains(randomTile));

            occupiedTiles.Add(randomTile);

            Vector2 randomPosition = randomTile * ObjectSize;
            Node2D newObject = (Node2D)Islands[i].Instantiate();
            newObject.Position = randomPosition;
            AddChild(newObject);
        }
    }

}