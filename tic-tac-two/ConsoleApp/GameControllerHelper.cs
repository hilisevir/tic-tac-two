using DAL;
using MenuSystem;

namespace ConsoleApp;

public static class GameControllerHelper
{
    public static string ChooseSavedGame()
    {
        var gameMenuItems = new List<MenuItem>();
        var gameDict = RepositoryHelper.GameRepository.GetGameNames();
        
        for (var i = 0; i < gameDict.Count; i++)
        {
            var keyIndex = i;
            gameMenuItems.Add(new MenuItem()
            {
                Title = gameDict[gameDict.Keys.ElementAt(i)],
                Shortcut = (i + 1).ToString(),
                MenuItemAction = () => gameDict.Keys.ElementAt(keyIndex).ToString()
            });
        }

        if (gameMenuItems.Count == 0)
        {
            Console.WriteLine("No saved games found");
            return "Exit!";
        }
        var loadMenu = new Menu(EMenuLevel.Secondary, "TIC-TAC-TWO Game Saves", 
            gameMenuItems, 
            isCustomMenu: true);

        return loadMenu.Run();
    }
    
    public static string ChooseConfiguration()
    {
        var configMenuItems = new List<MenuItem>();
        var configDict =  RepositoryHelper.ConfigRepository.GetConfigurationNames();
        
        for (var i = 0; i < configDict.Count; i++)
        {
            var keyIndex = i;
            configMenuItems.Add(new MenuItem()
            {
                Title = configDict[configDict.Keys.ElementAt(i)],
                Shortcut = (i + 1).ToString(),
                MenuItemAction = () => configDict.Keys.ElementAt(keyIndex).ToString()
            });
        }
    
        var configMenu = new Menu(EMenuLevel.Secondary, "TIC-TAC-TWO Game Config", 
            configMenuItems, 
            isCustomMenu: true);

        return configMenu.Run();
    }

    public static string ChooseGameType()
    {
        var typesMenuItems = new List<MenuItem>();
        var typesDict =  RepositoryHelper.GameTypeRepository.GetGameTypeNames();
        
        for (var i = 0; i < typesDict.Count; i++)
        {
            var keyIndex = i;
            typesMenuItems.Add(new MenuItem()
            {
                Title = typesDict[typesDict.Keys.ElementAt(i)],
                Shortcut = (i + 1).ToString(),
                MenuItemAction = () => typesDict.Keys.ElementAt(keyIndex).ToString()
            });
        }
        
        var typesMenu = new Menu(EMenuLevel.Secondary, "TIC-TAC-TWO Game Types", 
            typesMenuItems, 
            isCustomMenu: true);

        return typesMenu.Run();
    }
}