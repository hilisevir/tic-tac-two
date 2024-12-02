using DAL;
using GameBrain;
using MenuSystem;

namespace ConsoleApp;

public static class GameControllerLoadGame
{
    public static string MainLoop()
    {
        
        
        var chosenGameShortcut = ChooseSavedGame();
        if (!int.TryParse(chosenGameShortcut, out var gameNo))
        {
            return chosenGameShortcut;
        }
            
        var chosenGame = RepositoryHelper.GameRepository.GetGameByName(
            RepositoryHelper.GameRepository.GetGameNames()[gameNo]
        );
        
        var gridConstruct = new SlidingGrid(
            chosenGame.SlidingGrid.GridCenterX,
            chosenGame.SlidingGrid.GridCenterY,
            chosenGame.SlidingGrid.StartRow,
            chosenGame.SlidingGrid.EndRow,
            chosenGame.SlidingGrid.StartCol,
            chosenGame.SlidingGrid.EndCol);
        var gameInstance = new TicTacTwoBrain(chosenGame, gridConstruct);

        GameController.GameLoop(gridConstruct, gameInstance);
        
        return "Game Finished!";
    }
    
    private static string ChooseSavedGame()
    {
        var gameMenuItems = new List<MenuItem>();
        for (var i = 0; i < RepositoryHelper.GameRepository.GetGameNames().Count; i++)
        {
            var returnValue = i.ToString();
            gameMenuItems.Add(new MenuItem()
            {
                Title = RepositoryHelper.GameRepository.GetGameNames()[i],
                Shortcut = (i + 1).ToString(),
                MenuItemAction = () => returnValue
            });
        }
    
        var loadMenu = new Menu(EMenuLevel.Secondary, "TIC-TAC-TWO Game Saves", 
            gameMenuItems, 
            isCustomMenu: true);

        return loadMenu.Run();
    }
}