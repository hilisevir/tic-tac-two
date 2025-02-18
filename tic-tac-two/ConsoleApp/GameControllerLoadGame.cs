using DAL;
using GameBrain;
using MenuSystem;

namespace ConsoleApp;

public static class GameControllerLoadGame
{
    public static string MainLoop()
    {
        
        
        var chosenGameShortcut = GameControllerHelper.ChooseSavedGame();
        if (!int.TryParse(chosenGameShortcut, out var gameNo))
        {
            return chosenGameShortcut;
        }
            
        var chosenGame = RepositoryHelper.GameRepository.GetGameStateById(gameNo);

        if (chosenGame == null) return "Game Finished!";
        
        var gridConstruct = new SlidingGrid(
            chosenGame.SlidingGrid.GridCenterX,
            chosenGame.SlidingGrid.GridCenterY,
            chosenGame.SlidingGrid.StartRow,
            chosenGame.SlidingGrid.EndRow,
            chosenGame.SlidingGrid.StartCol,
            chosenGame.SlidingGrid.EndCol,
            chosenGame.SlidingGrid.GridHeight,
            chosenGame.SlidingGrid.GridWidth);
        
        var gameInstance = new TicTacTwoBrain(chosenGame, gridConstruct);
        
        GameController.GameLoop(gridConstruct, gameInstance);

        return "Game Finished!";
    }
}