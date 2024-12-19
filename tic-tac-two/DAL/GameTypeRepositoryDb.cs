using Domain;

namespace DAL;

public class GameTypeRepositoryDb(AppDbContext context) : IGameTypeRepository
{
    public Dictionary<int, string> GetGameTypeNames()
    {
        CheckAndCreateInitialGameTypes();

        return context.GameType
            .OrderBy(c => c.Name)
            .ToDictionary(c => c.Id, c => c.Name);
    }

    public void SaveGameType(GameType gameType)
    {
        // Проверяем, существует ли тип игры с таким же ID
        var existing = context.GameType.SingleOrDefault(gt => gt.Id == gameType.Id);
        
        if (existing != null) return;
        
        context.GameType.Add(new GameType
        {
            Name = gameType.Name,
            Description = gameType.Description
        });

        context.SaveChanges();
    }

    public void CheckAndCreateInitialGameTypes()
    {
        if (context.GameType.Any()) return;
        var hardcodedGameTypes = GameTypeHardcoded.GetInitialGameTypes();

        foreach (var gameType in hardcodedGameTypes)
        {
            if (!context.GameType.Any(gt => gt.Name == gameType.Name))
            {
                context.GameType.Add(gameType);
            }
        }

        context.SaveChanges();
    }

    public GameType GetGameTypeById(int gameTypeId)
    {
        var gameType = context.GameType
            .FirstOrDefault(c => c.Id == gameTypeId);

        return gameType ?? throw new InvalidOperationException();
        
    }
}