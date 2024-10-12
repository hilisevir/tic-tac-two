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
    
        var gameInstance = new TicTacTwoBrain(chosenConfig);
    
        


        do
        {
            ConsoleUI.Visualizer.DrawBoard(gameInstance);
            
            Console.Write("Give me coordinates <x,y>:");
            var coordinates = Console.ReadLine()!;
            var coordinateSplit = coordinates.Split(',');
            var inputX = int.Parse(coordinateSplit[0]);
            var inputY = int.Parse(coordinateSplit[1]);
            gameInstance.MakeAMove(inputX, inputY);
            
        } while (true);
    
        // loop
        // draw the board again
        // ask the input again, validate input
        // is game over?
        return "";
    }

    private  static string ChooseConfiguration()
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