using MenuSystem;

namespace ConsoleApp;

public static class Menus
{
    private static Menu OptionsMenu => new Menu(
        EMenuLevel.Secondary,
        "TIC-TAC-TWO Options", [
        new MenuItem()
        {
            Shortcut = "C",
            Title = "Create New Game Configuration",
            MenuItemAction = CreateConfigController.MainLoopCreateConfig
        },
        new MenuItem()
        {
            Shortcut = "D",
            Title = "Delete Game Configuration",
            MenuItemAction = DeleteConfigController.MainLoopDeleteConfig
        },
        new MenuItem()
        {
            Shortcut = "B",
            Title = "Delete Save",
            MenuItemAction = DeleteGameController.MainLoopDeleteGame
        }
        
    ]);
    public static Menu MainMenu => new Menu(
        EMenuLevel.Main,
        "TIC-TAC-TWO", [
    
        new MenuItem()
        {
            Shortcut = "N",
            Title = "New Game",
            MenuItemAction = GameControllerNewGame.MainLoop
        },
        new MenuItem()
        {
            Shortcut = "L",
            Title = "Load Game",
            MenuItemAction = GameControllerLoadGame.MainLoop
        },
        new MenuItem()
        {
            Shortcut = "O",
            Title = "Options",
            MenuItemAction = OptionsMenu.Run
        }
    ]);
}