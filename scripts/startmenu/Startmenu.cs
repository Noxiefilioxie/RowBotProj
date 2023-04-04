using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowBot.scripts.startmenu
{

    public partial class Startmenu : Control
    {
        [Export]
        public PackedScene MainScene;
        public override void _Ready()
        {
            // Connect the button signals to their respective methods
            GetNode<Button>("StartButton").Connect("pressed", new Callable(this, nameof(StartGame)));
            GetNode<Button>("CreditsButton").Connect("pressed", new Callable(this, nameof(ShowCredits)));
            GetNode<Button>("ExitButton").Connect("pressed", new Callable(this, nameof(ExitGame)));

            GetNode<RichTextLabel>("CreditsLabel").Visible = false;

        }

        private void StartGame()
        {
            GetTree().ChangeSceneToFile(MainScene.ResourcePath);
            GD.Print("Resuming game...");
            // Your resume game logic here
        }

        private void ShowCredits()
        {
            GD.Print("Credits:");
            GD.Print("Game developed by: Ugly Leopard Entertainment");
            GD.Print("Lead Programmer: Jonk");
            GD.Print("Lead Designer: Calle");
            GD.Print("CIO: Dick");
            GD.Print(GetNode<RichTextLabel>("CreditsLabel"));
            GetNode<RichTextLabel>("CreditsLabel").Visible = !GetNode<RichTextLabel>("CreditsLabel").Visible;

        }

        private void ExitGame()
        {
            GD.Print("Exiting game...");
            GetTree().Quit(); // Exit the game
        }
    }
}
