using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowBot.scripts.inventory
{
    public partial class SlotController : Panel
    {
        [Export]
        public PackedScene ItemScene;
        public override void _Ready()
        {

            //Get list of items in users inventory

            //Add some sample items for testing purposes,
            Inventory inventory = new Inventory();
            Item item1 = new Item("Item1PlaceHolder", "res://assets/images/inventory/Item1PlaceHolder.png");
            Item item2 = new Item("Item2PlaceHolder", "res://assets/images/inventory/Item2PlaceHolder.png");
            inventory.AddItem(item1);
            inventory.AddItem(item2);
        }
    }
}
