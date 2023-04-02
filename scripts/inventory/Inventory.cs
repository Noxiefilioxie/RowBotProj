using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowBot.scripts.inventory
{
    public class Inventory
    {
        private List<Item> items;

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
