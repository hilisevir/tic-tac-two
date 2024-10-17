namespace DAL;

public static class FileHelper
{
    public static string _basePath = Environment
                                  .GetFolderPath(Environment.SpecialFolder.ApplicationData)
                              + Path.DirectorySeparatorChar + "tic-tac-two" + Path.DirectorySeparatorChar;

    public static string ConfigExtension = ".config.json";
    public static string GameExtension = ".game.json";
}