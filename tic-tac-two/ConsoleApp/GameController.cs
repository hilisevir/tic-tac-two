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
        var gridConstruct = new SlidingGrid(chosenConfig);
        

        do
        {
            bool flag;
            do
            {
                
                Console.Write("Choose initial grid position by providing coordinates <x,y>:");
                var gridPosition = Console.ReadLine()!;
                flag = gameInstance.PlaceGrid(gridConstruct, gridPosition);
            } while (flag); // TODO get rid of do while and true/false
            do
            {
                
                Console.Write("Choose initial grid position by providing coordinates <x,y>:");
                var gridPosition = Console.ReadLine()!;
                flag = gameInstance.PlaceGrid(gridConstruct, gridPosition);
            } while (flag);
            
            ConsoleUI.Visualizer.DrawBoard(gameInstance, gridConstruct);
            
            do
            {
                
                Console.Write("Give me coordinates <x,y>:");
                var coordinates = Console.ReadLine()!;
                flag = gameInstance.MakeAMove(coordinates);
                
            } while (flag);
            

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