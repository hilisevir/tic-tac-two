using MenuSystem;

namespace ConsoleApp;

public static class Menus
{
    public static Menu OptionsMenu => 
    new Menu(
        EMenuLevel.Secondary,
        "TIC-TAC-TWO", [
        
        new MenuItem()
        {
            Shortcut = "X",
            Title = "X starts",
            MenuItemAction = DummyMethod
        },
        new MenuItem()
        {
            Shortcut = "O",
            Title = "O starts",
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
            Title = "Options"
        }
    ]);

    public static string DummyMethod()
    {
        Console.WriteLine("Just press any key to continue...");
        Console.ReadKey();
        return "foobar";
    }
}