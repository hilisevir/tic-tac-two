using System.Text.Json;
using GameBrain;

namespace DAL;

public class GameRepositoryJson : IGameRepository
{
    public List<string> GetGameNames()
    {
        var res = new List<string>();
        foreach (var fullFileName in Directory.GetFiles(FileHelper._basePath, "*" + FileHelper.GameExtension))
        {
            var filenameParts = Path.GetFileNameWithoutExtension(fullFileName);
            var primaryName = Path.GetFileNameWithoutExtension(filenameParts);
            res.Add(primaryName);
        }

        return res;
    }

    public void SaveGame(GameState gameState)
    {
        var gameStateString = gameState.ToString();
        var timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss");
        var fileName = FileHelper._basePath + 
                       gameState.Name + " " + 
                       timestamp + 
                       FileHelper.GameExtension;
        
        File.WriteAllText(fileName, gameStateString);
        
    }

    public GameState GetGameByName(string gameName)
    {
        var gameJsonStr = File.ReadAllText(FileHelper._basePath + gameName + FileHelper.GameExtension);
        
        
        var gameState = JsonSerializer.Deserialize<GameState>(gameJsonStr);
        return gameState;
    }
}