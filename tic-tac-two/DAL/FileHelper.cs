namespace DAL;

public static class FileHelper
{
    public static readonly string BasePath = Environment
                                                 .GetFolderPath(Environment.SpecialFolder.ApplicationData)
                                             + Path.DirectorySeparatorChar + "tic-tac-two" + Path.DirectorySeparatorChar;

    public const string ConfigExtension = ".config.json";
    public const string GameExtension = ".game.json";
    public const string GameTypeExtension = ".gametype.json";
    

    public static readonly string ConfigIdCounterFile = BasePath + "id_counter_config.txt";
    public static readonly string GameIdCounterFile = BasePath + "id_counter_game.txt";
    
    public static string GetUniqueGameName()
    {
        var count = 0;
        var res = RepositoryHelper.GameRepository.GetGameNames();
        foreach (var t in res.Values)
        {
            if (t.Contains("New Save"))
            {
                count++;
            }
        }
        return "New Save " + (count + 1);
    }
}