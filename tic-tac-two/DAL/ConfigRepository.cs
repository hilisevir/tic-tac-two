using GameBrain;

namespace DAL;

public class ConfigRepository
{
    private List<GameConfiguration> _gameConfigurations =
    [
        new GameConfiguration()
        {
            Name = "Classical"

        },

        new GameConfiguration()
        {
            // data for this game is taken from options
            Name = "Custom",
            BoardSizeWidth = 10,
            BoardSizeHeight = 10,
            GridSizeHeight = 5,
            GridSizeWidth = 5,
            WinCondition = 3,
            GridColor = "Red",
            Player1Pieces = [EGamePiece.X, EGamePiece.X, EGamePiece.X, EGamePiece.X, EGamePiece.X],
            Player2Pieces = [EGamePiece.O, EGamePiece.O, EGamePiece.O, EGamePiece.O, EGamePiece.O]

        },

        new GameConfiguration()
        {
            // one of the game alternative
            Name = "Big 10x10",
            BoardSizeWidth = 11,
            BoardSizeHeight = 11,
            GridSizeHeight = 6,
            GridSizeWidth = 6,
            WinCondition = 4,
            MovePieceAfterNMoves = 2,
            Player1Pieces = [EGamePiece.X, EGamePiece.X, EGamePiece.X, EGamePiece.X, EGamePiece.X, EGamePiece.X, EGamePiece.X],
            Player2Pieces = [EGamePiece.O, EGamePiece.O, EGamePiece.O, EGamePiece.O, EGamePiece.O, EGamePiece.O, EGamePiece.O],

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