using DAL;

namespace ConsoleApp;

public static class DeleteConfigController
{
    public static string MainLoopDeleteConfig()
    {
        var chosenConfigShortcut = GameControllerHelper.ChooseConfiguration();

        if (!int.TryParse(chosenConfigShortcut, out var configNo))
        {
            return chosenConfigShortcut;
        }
        
        var chosenConfig = RepositoryHelper.ConfigRepository.GetGameConfigurationById(configNo);
        
        Console.Write("Are you sure you want to delete configuration? (y/n) ");
        if (Console.ReadLine()?.ToLower() == "n")
        {
            return "Exit";
        }
        
        RepositoryHelper.ConfigRepository.DeleteConfiguration(chosenConfig);
        Console.WriteLine($"Configuration '{chosenConfig.Name}' has been successfully deleted.");

        return "";
    }
}