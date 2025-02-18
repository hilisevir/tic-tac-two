using DAL;
using Domain;
using GameBrain;

namespace ConsoleApp;

public static class GameControllerNewGame
{
    public static string MainLoop()
    {
        var chosenConfigShortcut = GameControllerHelper.ChooseConfiguration();

        if (!int.TryParse(chosenConfigShortcut, out var configNo))
        {
            return chosenConfigShortcut;
        }
    
        var chosenConfig = RepositoryHelper.ConfigRepository.GetGameConfigurationById(configNo);
        
        var gridConstruct = new SlidingGrid(chosenConfig);
        var gameInstance = new TicTacTwoBrain(chosenConfig, gridConstruct);

        do
        {
            Console.Write("Choose initial grid position by providing coordinates <x,y>:");
            var gridPosition = Console.ReadLine()!;
            var coordinatesGrid = gridPosition.Split(',');
            if (!gameInstance.CheckCoordinates(coordinatesGrid)) continue;
            var x = int.Parse(coordinatesGrid[0]);
            var y = int.Parse(coordinatesGrid[1]);

            if (!gameInstance.PlaceAGrid(gridConstruct, x, y)) continue;
            
            break;
            
        } while (true);
        
        GameController.GameLoop(gridConstruct, gameInstance);
        
        return "Game Finished!";
    }
}