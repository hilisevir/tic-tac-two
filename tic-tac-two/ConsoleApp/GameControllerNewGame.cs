using DAL;
using GameBrain;
using MenuSystem;

namespace ConsoleApp;

public static class GameControllerNewGame
{
    public static string MainLoop()
    {
        var chosenConfigShortcut = ChooseConfiguration();

        if (!int.TryParse(chosenConfigShortcut, out var configNo))
        {
            return chosenConfigShortcut;
        }
    
        var chosenConfig = RepositoryHelper.ConfigRepository.GetConfigurationByName(
            RepositoryHelper.ConfigRepository.GetConfigurationNames()[configNo]
        );
        
        var gridConstruct = new SlidingGrid(chosenConfig);
        var gameInstance = new TicTacTwoBrain(chosenConfig, gridConstruct);
        
        gameInstance.PlaceAGrid(gridConstruct);
        GameController.GameLoop(gridConstruct, gameInstance);
        return "Game Finished!";
    }
    
    private static string ChooseConfiguration()
    {
        var configMenuItems = new List<MenuItem>();
        for (var i = 0; i < RepositoryHelper.ConfigRepository.GetConfigurationNames().Count; i++)
        {
            var returnValue = i.ToString();
            configMenuItems.Add(new MenuItem()
            {
                Title = RepositoryHelper.ConfigRepository.GetConfigurationNames()[i],
                Shortcut = (i + 1).ToString(),
                MenuItemAction = () => returnValue
            });
        }
    
        var configMenu = new Menu(EMenuLevel.Secondary, "TIC-TAC-TWO Game Config", 
            configMenuItems, 
            isCustomMenu: true);

        return configMenu.Run();
    }
    
}