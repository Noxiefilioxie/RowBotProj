using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowBot.scripts.inventory
{
    public partial class Inventory : Control
    {
        private List<Item> items;

        public override void _Ready()
        {

        }

        public Inventory()
        {
            items = new List<Item>();
        }
        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public static void ClearItem(string item)
        {
        }


    }

    public class Item
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public Item(string name, string imagePath)
        {
            Name = name;
            ImagePath = imagePath;
        }
    }

}
