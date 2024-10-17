using DAL;
using GameBrain;
using MenuSystem;

namespace ConsoleApp;

public class Menus
{ 
    public static Menu OptionsMenu => new Menu(
        EMenuLevel.Secondary,
        "TIC-TAC-TWO", [
        new MenuItem()
        {
            Shortcut = "C",
            Title = "Create New Game Configuration",
            MenuItemAction = ConfigCreation
        },
        new MenuItem()
        {
            Shortcut = "B",
            Title = "Change Game Configuration",
            MenuItemAction = DummyMethod
        },
        new MenuItem()
        {
            Shortcut = "D",
            Title = "Delete Game Configuration",
            MenuItemAction = DummyMethod
        }
        
    ]);
    public static Menu MainMenu => new Menu(
        EMenuLevel.Main,
        "TIC-TAC-TWO", [
    
        new MenuItem()
        {
            Shortcut = "N",
            Title = "New Game",
            MenuItemAction = GameController.MainLoop
        },
        new MenuItem()
        {
            Shortcut = "O",
            Title = "Options",
            MenuItemAction = OptionsRun
        }
    ]);

    public static Menu GameMenu => new Menu(
        EMenuLevel.InGame,
        "TIC_TAC_TWO", [
            new MenuItem()
            {
                Shortcut = "G",
                Title = "Move a Grid",
                MenuItemAction = TicTacTwoBrain.MoveAGrid
            },
            new MenuItem()
            {
                Shortcut = "P",
                Title = "Place a Piece",
                MenuItemAction = TicTacTwoBrain.MakeAMove
            },
            new MenuItem()
            {
                Shortcut = "C",
                Title = "Change Position for the Piece",
                MenuItemAction = TicTacTwoBrain.ChangePiecePosition
            },
            new MenuItem()
            {
                Shortcut = "S",
                Title = "Save Game",
                MenuItemAction = DummyMethod
            }
        ]);
    

    public static string DummyMethod()
    {
        Console.WriteLine("Just press any key to continue...");
        Console.ReadKey();
        return "foobar";
    }

    public static string OptionsRun()
    {
        OptionsMenu.Run();
        return "R";
    }

    public static string ConfigCreation()
    {
        OptionsController.MainLoop();
        
        return "";
    }
    
    
}