using Godot;
using RowBot.scripts.inventory;
using System;

public partial class UserInterface : CanvasLayer
{
    // Called when the node enters the scene tree for the first time.

    [Export]
    public NodePath InventoryPath;

    private Node2D _inventory;

    public override void _Ready()
    {
        _inventory = GetNode<Node2D>(InventoryPath);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.IsActionPressed("Tab"))
        {
            _inventory.Visible = !_inventory.Visible;
        }
    }

}
