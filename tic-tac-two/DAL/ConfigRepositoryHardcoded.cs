using Domain;
using GameBrain;

namespace DAL;

public class ConfigRepositoryHardcoded : IConfigRepository
{
    private readonly List<GameConfiguration> _gameConfigurations =
    [
        new GameConfiguration()
        {
            Id = 1,
            Name = "Classical"

        },

        new GameConfiguration()
        {
            // data for this game is taken from options
            Id = 2,
            Name = "Custom",
            BoardWidth = 7,
            BoardHeight = 5,
            GridHeight = 4,
            GridWidth = 4,
            WinCondition = 3,
            MovePieceAfterNMoves = 4,
            Player1PieceAmount = 5,
            Player2PieceAmount = 5

        },

        new GameConfiguration()
        {
            // one of the game alternative
            Id = 3,
            Name = "Big 10x10",
            BoardWidth = 10,
            BoardHeight = 10,
            GridHeight = 6,
            GridWidth = 6,
            WinCondition = 4,
            MovePieceAfterNMoves = 2,
            Player1PieceAmount = 7,
            Player2PieceAmount = 7

        }
    ];

    public Dictionary<int, string> GetConfigurationNames()
    {
        return _gameConfigurations.ToDictionary(config => config.Id, config => config.Name);
    }

    public GameConfiguration GetGameConfigurationById(int configId)
    {
        return _gameConfigurations.Single(config => config.Id == configId);
    }

    public Configuration GetConfigurationById(int configId)
    {
        throw new NotImplementedException();
    }

    public void SaveConfiguration(GameConfiguration newConfig)
    {
        throw new NotImplementedException();
    }

    public void DeleteConfiguration(GameConfiguration config)
    {
        throw new NotImplementedException();
    }
}