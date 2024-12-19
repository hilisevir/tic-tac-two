using System.Text.Json;
using Domain;
using GameBrain;

namespace DAL;

public class GameRepositoryJson : IGameRepository
{
    public Dictionary<int, string> GetGameNames()
    {
        var res = new Dictionary<int, string>();
        foreach (var fullFileName in Directory.GetFiles(FileHelper.BasePath, "*" + FileHelper.GameExtension))
        {
            var json = File.ReadAllText(fullFileName);
            
            var game = JsonSerializer.Deserialize<GameState>(json);
            if (game != null) res.Add(game.Id, game.Name);
        }

        return res;
    }

    public void SaveGame(GameState gameState)
    {
        var gameDict = GetGameNames();
        foreach (var game in gameDict)
        {
            var oldFileName = FileHelper.BasePath + gameDict[game.Key] + FileHelper.GameExtension;
            if (File.Exists(oldFileName))
            {
                File.Delete(oldFileName);
            }
        }
        
        
        if (gameState.Id < 1)
        {
            gameState.Id = GetNextId();
        }
        
        var gameStateString = gameState.ToString();
        var fileName = FileHelper.BasePath + 
                       gameState.Name + 
                       FileHelper.GameExtension;
        
        File.WriteAllText(fileName, gameStateString);
        
    }

    public SaveGame? GetSaveGameById(int gameId)
    {
        var gameDict = GetGameNames();
        var gameJsonStr = File.ReadAllText(FileHelper.BasePath + gameDict[gameId] + FileHelper.GameExtension);
        
        
        var gameState = JsonSerializer.Deserialize<SaveGame>(gameJsonStr);
        return gameState;
    }

    public void DeleteGame(int gameId)
    {
        var gameDict = GetGameNames();

        if (!gameDict.TryGetValue(gameId, out var gameName)) return;
        var fileName = FileHelper.BasePath + gameName + FileHelper.GameExtension;

        if (!File.Exists(fileName)) return;
        File.Delete(fileName);
    }

    
    public GameState? GetGameStateById (int gameId)
    {
        var gameDict = GetGameNames();
        var gameJsonStr = File.ReadAllText(FileHelper.BasePath + gameDict[gameId] + FileHelper.GameExtension);
        
        
        var gameState = JsonSerializer.Deserialize<GameState>(gameJsonStr);
        return gameState;
    }
    
    private static int GetNextId()
    {
        if (!File.Exists(FileHelper.GameIdCounterFile))
        {
            File.WriteAllText(FileHelper.GameIdCounterFile, "0");
        }
        
        var currentId = int.Parse(File.ReadAllText(FileHelper.GameIdCounterFile));
        
        var nextId = currentId + 1;
        File.WriteAllText(FileHelper.GameIdCounterFile, nextId.ToString());

        return nextId;
    }
}