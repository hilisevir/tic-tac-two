using DAL;
namespace ConsoleApp;

public static class DeleteGameController
{
    public static string MainLoopDeleteGame()
    {
        var chosenGameShortcut = GameControllerHelper.ChooseSavedGame();

        if (!int.TryParse(chosenGameShortcut, out var configNo))
        {
            return chosenGameShortcut;
        }
        
        var chosenGame = RepositoryHelper.GameRepository.GetGameStateById(configNo);
        
        Console.Write("Are you sure you want to delete game? (y/n) ");
        if (Console.ReadLine()?.ToLower() == "n")
        {
            return "Exit";
        }

        if (chosenGame == null) return "";
        RepositoryHelper.GameRepository.DeleteGame(chosenGame.Id);
        Console.WriteLine($"Configuration '{chosenGame.Name}' has been successfully deleted.");

        return "";
    }
}