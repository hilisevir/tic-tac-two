using DAL;
using GameBrain;
using MenuSystem;

namespace ConsoleApp;

public static class GameController
{
    private static readonly ConfigRepository ConfigRepository = new ConfigRepository();
    public static string MainLoop()
    {
        var chosenConfigShortcut = ChooseConfiguration();

        if (!int.TryParse(chosenConfigShortcut, out var configNo))
        {
            return chosenConfigShortcut;
        }
    
        var chosenConfig = ConfigRepository.GetConfigurationByName(
            ConfigRepository.GetConfigurationNames()[configNo]
        );
        
        var gridConstruct = new SlidingGrid(chosenConfig);
        var gameInstance = new TicTacTwoBrain(chosenConfig, gridConstruct);
        
        gameInstance.PlaceAGrid(gridConstruct);
        
        var counter = 0;
        do
        {
            // Console.Clear();
            ConsoleUI.Visualizer.DrawBoard(gameInstance, gridConstruct);
            if (counter >= 2)
            {
                Menus.GameMenu.Run();
            }
            else
            {
                TicTacTwoBrain.MakeAMove();
            }
            counter++;

        } while (true);
    
        // loop
        // draw the board again
        // ask the input again, validate input
        // is game over?
        return "";
    }

    private static string ChooseConfiguration()
    {
        var configMenuItems = new List<MenuItem>();
        for (var i = 0; i < ConfigRepository.GetConfigurationNames().Count; i++)
        {
            var returnValue = i.ToString();
            configMenuItems.Add(new MenuItem()
            {
                Title = ConfigRepository.GetConfigurationNames()[i],
                Shortcut = (i + 1).ToString(),
                MenuItemAction = () => returnValue
            });
        }
    
        var configMenu = new Menu(EMenuLevel.Secondary, "TIC-TAC-TWO Game Config", configMenuItems);

        return configMenu.Run();
    }
    
}