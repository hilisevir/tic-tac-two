using GameBrain;

namespace DAL;

public class ConfigRepository
{
    private List<GameConfiguration> _gameConfigurations =
    [
        new GameConfiguration()
        {
            Name = "Classical",
            GridColor = "Blue"

        },

        new GameConfiguration()
        {
            // data for this game is taken from options
            Name = "Custom",
            BoardSizeWidth = 5,
            BoardSizeHeight = 5,
            GridSizeHeight = 3,
            GridSizeWidth = 3,
            WinCondition = 3,
            GridColor = "Red"

        },

        new GameConfiguration()
        {
            // one of the game alternative
            Name = "Big 10x10",
            BoardSizeWidth = 10,
            BoardSizeHeight = 10,
            GridSizeHeight = 5,
            GridSizeWidth = 5,
            WinCondition = 4,
            MovePieceAfterNMoves = 3

        }
    ];

    public List<String> GetConfigurationNames()
    {
        return _gameConfigurations.Select(config => config.Name).ToList();
    }

    public GameConfiguration GetConfigurationByName(string name)
    {
        return _gameConfigurations.Single(config => config.Name == name);
    }
}