using Domain;
using GameBrain;

namespace DAL;

public class ConfigRepositoryDb(AppDbContext context) : IConfigRepository
{
    public Dictionary<int, string> GetConfigurationNames()
    {
        CheckAndCreateInitialConfig();
        
        return context.Configurations
            .OrderBy(c => c.Name)
            .ToDictionary(c => c.Id, c => c.Name);
    }

    private void CheckAndCreateInitialConfig()
    {
        var hardCodedRepo = new ConfigRepositoryHardcoded();
        var optionNames = hardCodedRepo.GetConfigurationNames();

        if (context.Configurations.Any()) return;
        
        foreach (var optionName in optionNames)
        {
            var gameOption = hardCodedRepo.GetGameConfigurationById(optionName.Key);
            SaveConfiguration(gameOption);
        }
    }
    
    public GameConfiguration GetGameConfigurationById(int configId)
    {
        var configuration = context.Configurations
            .FirstOrDefault(c => c.Id == configId);


        if (configuration != null)
        {
            GameConfiguration config = new GameConfiguration()
            {
                Id = configuration.Id,
                Name = configuration.Name,
                BoardWidth = configuration.BoardWidth,
                BoardHeight = configuration.BoardHeight,
                GridWidth = configuration.GridWidth,
                GridHeight = configuration.GridHeight,
                WinCondition = configuration.WinCondition,
                MovePieceAfterNMoves = configuration.MovePieceAfterNMoves,
                Player1PieceAmount = configuration.Player1PieceAmount,
                Player2PieceAmount = configuration.Player2PieceAmount
            };
        
            return config;
        }
        
        throw new KeyNotFoundException($"Game configuration with id {configId} not found");
    }
    
    public Configuration GetConfigurationById(int configId)
    {
        var configuration = context.Configurations
            .FirstOrDefault(c => c.Id == configId);
        
        return configuration ?? throw new InvalidOperationException($"Game configuration with id {configId} not found");
            
    }
    
    public void SaveConfiguration(GameConfiguration gameConfig)
    {
        context.Configurations.Add(new Configuration()
        {
            Name = gameConfig.Name,
            BoardHeight = gameConfig.BoardHeight,
            BoardWidth = gameConfig.BoardWidth,
            GridHeight = gameConfig.GridHeight,
            GridWidth = gameConfig.GridWidth,
            MovePieceAfterNMoves = gameConfig.MovePieceAfterNMoves,
            Player1PieceAmount = gameConfig.Player1PieceAmount,
            Player2PieceAmount = gameConfig.Player2PieceAmount,
            WinCondition = gameConfig.WinCondition,
        });
        
        context.SaveChanges();
    }

    public void DeleteConfiguration(GameConfiguration gameConfig)
    {
        var configuration = context.Configurations
            .FirstOrDefault(c => c.Name == gameConfig.Name);

        if (configuration == null)
        {
            Console.WriteLine($"Configuration '{gameConfig.Name}' not found. Nothing to delete.");
            return;
        }
        
        context.Configurations.Remove(configuration);
        context.SaveChanges();
    }
    
}