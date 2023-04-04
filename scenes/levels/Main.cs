using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node2D
{
    [Export]
    public PackedScene[] Islands;
    [Export]
    public int MapSize = 10000;
    public int ObjectSize = 60;
    private HashSet<Rect2> occupiedTiles = new HashSet<Rect2>();


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
    int maxAttempts = 1000;

    for (int i = 0; i < Islands.Length; i++)
    {
        Node2D newObject = (Node2D)Islands[i].Instantiate();

        // Get the island size
        CollisionPolygon2D islandCollisionPolygon = newObject.GetNode<CollisionPolygon2D>("Area2D/CollisionPolygon2D");
        Vector2 islandSize = GetSizeFromPolygon(islandCollisionPolygon);

        Vector2 randomPosition;
        int attempts = 0;

        // Generate random positions until an unoccupied position is found or the maximum number of attempts is reached.
        do
        {
            float randomX = (float)random.NextDouble() * (MapSize - islandSize.X);
            float randomY = (float)random.NextDouble() * (MapSize - islandSize.Y);
            randomPosition = new Vector2(randomX, randomY);
            attempts++;
        } while (!IsPositionAvailable(randomPosition, islandSize) && attempts < maxAttempts);

        if (attempts >= maxAttempts)
        {
            // If the maximum number of attempts is reached, skip placing the current island.
            continue;
        }

        AddOccupiedArea(randomPosition, islandSize);

        newObject.Position = randomPosition;
        AddChild(newObject);
    }
}



private Vector2 GetSizeFromPolygon(CollisionPolygon2D polygon)
{
    Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
    Vector2 max = new Vector2(float.MinValue, float.MinValue);

    for (int i = 0; i < polygon.Polygon.Length; i++)
    {
        Vector2 point = polygon.Polygon[i];
        min.X = Mathf.Min(min.X, point.X);
        min.Y = Mathf.Min(min.Y, point.Y);
        max.X = Mathf.Max(max.X, point.X);
        max.Y = Mathf.Max(max.Y, point.Y);
    }

    Vector2 size = max - min;
    // Round the size to the nearest grid unit
    size.X = Mathf.Ceil(size.X / ObjectSize) * ObjectSize;
    size.Y = Mathf.Ceil(size.Y / ObjectSize) * ObjectSize;

    return size;
}

private bool IsPositionAvailable(Vector2 position, Vector2 islandSize)
{
    Rect2 newIslandRect = new Rect2(position, islandSize);

    foreach (Rect2 existingIslandRect in occupiedTiles)
    {
        if (existingIslandRect.Intersects(newIslandRect))
        {
            return false;
        }
    }

    return true;
}

private void AddOccupiedArea(Vector2 position, Vector2 islandSize)
{
    Rect2 newIslandRect = new Rect2(position, islandSize);
    occupiedTiles.Add(newIslandRect);
}




}