using System.Text.Json;
using Domain;

namespace DAL;

public class GameTypeRepositoryJson : IGameTypeRepository
{
    public Dictionary<int, string> GetGameTypeNames()
    {
        CheckAndCreateInitialGameTypes();

        var res = new Dictionary<int, string>();
        foreach (var fullFileName in Directory.GetFiles(FileHelper.BasePath, "*" + FileHelper.GameTypeExtension))
        {
            var json = File.ReadAllText(fullFileName);
            var game = JsonSerializer.Deserialize<GameType>(json);
            if (game != null) res.Add(game.Id, game.Name);
        }

        return res;
    }

    public void SaveGameType(GameType gameType)
    {
        var fileName = Path.Combine(FileHelper.BasePath, $"{gameType.Name}{FileHelper.GameTypeExtension}");
        var json = JsonSerializer.Serialize(gameType);
        File.WriteAllText(fileName, json);
    }

    public void CheckAndCreateInitialGameTypes()
    {
        var existingFiles = Directory.GetFiles(FileHelper.BasePath, "*" + FileHelper.GameTypeExtension);

        if (existingFiles.Length != 0) return;
        
        var hardcodedGameTypes = GameTypeHardcoded.GetInitialGameTypes();
        foreach (var gameType in hardcodedGameTypes)
        {
            SaveGameType(gameType);
        }
    }

    public GameType GetGameTypeById(int gameTypeId)
    {
        var gameTypeDict = GetGameTypeNames();
        var gameTypeJsonStr = File.ReadAllText(FileHelper.BasePath + gameTypeDict[gameTypeId] + FileHelper.GameTypeExtension);
        var gameType = JsonSerializer.Deserialize<GameType>(gameTypeJsonStr);
        return gameType ?? throw new InvalidOperationException();
    }
}