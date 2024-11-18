using DAL;
using GameBrain;
using MenuSystem;

namespace ConsoleApp;

public static class Menus
{ 
    public static Menu OptionsMenu => new Menu(
        EMenuLevel.Secondary,
        "TIC-TAC-TWO Options", [
        new MenuItem()
        {
            Shortcut = "C",
            Title = "Create New Game Configuration",
            MenuItemAction = OptionsController.MainLoop
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
            MenuItemAction = OptionsMenu.Run
        }
    ]);
    
    public static string DummyMethod()
    {
        Console.WriteLine("Just press any key to continue...");
        Console.ReadKey();
        return "foobar";
    }
    
}