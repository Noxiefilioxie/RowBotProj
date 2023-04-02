using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowBot.scripts.inventory
{
    public partial class ItemController : Node2D
    {
        [Export]
        public TextureRect Texture;
        public override void _Ready()
        {
            var rand = GD.Randi()%2;
            if(GD.Randi() % 2 == 0)
            {
                ///Sätt texturerect.textureproperty ironsword
                Texture.Texture = ImageTexture.CreateFromImage(Image.LoadFromFile("res://assets/images/inventory/Item1PlaceHolder.png"));
            } else
            {
                Texture.Texture = ImageTexture.CreateFromImage(Image.LoadFromFile("res://assets/images/inventory/Item2PlaceHolder.png"));

            }
        }
    }
}
