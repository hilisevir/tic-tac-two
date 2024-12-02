using System.Text.Json;
using Domain;
using GameBrain;

namespace DAL;

public class ConfigRepositoryDb : IConfigRepository
{
    private readonly AppDbContext _context;

    public ConfigRepositoryDb(AppDbContext context)
    {
        _context = context;
    }
    
    public List<string> GetConfigurationNames()
    {
        ChecckAndCreateInitialConfig();
        
        return _context.Configurations
            .OrderBy(c => c.Name)
            .Select(c => c.Name)
            .ToList();
    }

    private void ChecckAndCreateInitialConfig()
    {
        var hardCodedRepo = new ConfigRepositoryHardcoded();
        var optionNames = hardCodedRepo.GetConfigurationNames();

        if (!_context.Configurations.Any())
        {
            foreach (var optionName in optionNames)
            {
                var gameOption = hardCodedRepo.GetConfigurationByName(optionName);
                SaveConfiguration(gameOption);
            }
        }
    }
    
    public GameConfiguration GetConfigurationByName(string name)
    {
        var configuration = _context.Configurations
            .FirstOrDefault(c => c.Name == name);
        
        
        GameConfiguration config = new GameConfiguration()
        {
            Name = configuration.Name,
            BoardSizeWidth = configuration.BoardWidth,
            BoardSizeHeight = configuration.BoardHeight,
            GridSizeWidth = configuration.GridWidth,
            GridSizeHeight = configuration.GridHeight,
            WinCondition = configuration.WinCondition,
            MovePieceAfterNMoves = configuration.MovePieceAfterNMoves,
            Player1Pieces = JsonSerializer.Deserialize<List<EGamePiece>>(configuration.Player1Pieces),
            Player2Pieces = JsonSerializer.Deserialize<List<EGamePiece>>(configuration.Player2Pieces)
        };
        
        return config;
    }
    
    public void SaveConfiguration(GameConfiguration gameConfig)
    {
        _context.Configurations.Add(new Configuration()
        {
            Name = gameConfig.Name,
            BoardHeight = gameConfig.BoardSizeHeight,
            BoardWidth = gameConfig.BoardSizeWidth,
            GridHeight = gameConfig.GridSizeHeight,
            GridWidth = gameConfig.GridSizeWidth,
            MovePieceAfterNMoves = gameConfig.MovePieceAfterNMoves,
            Player1Pieces = gameConfig.PiecesToString(gameConfig.Player1Pieces),
            Player2Pieces = gameConfig.PiecesToString(gameConfig.Player2Pieces),
            WinCondition = gameConfig.WinCondition,
        });
        
        _context.SaveChanges();
    }
    
}