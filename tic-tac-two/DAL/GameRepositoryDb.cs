using System.Text.Json;
using Domain;
using GameBrain;

namespace DAL;

public class GameRepositoryDb : IGameRepository
{
    private readonly AppDbContext _context;

    public GameRepositoryDb(AppDbContext context)
    {
        _context = context;
    }
    
    public List<string> GetGameNames()
    {
        return _context.SaveGames
            .OrderBy(c => c.Name)
            .Select(c => c.Name)
            .ToList();
    }

    public void SaveGame(GameState gameState)
    {
        _context.SaveGames.Add(new SaveGame()
        {
            Name = gameState.Name,
            GameBoard = gameState.BoardToString(),
            NextMoveBy = JsonSerializer.Serialize(gameState.NextMoveBy),
            Player1PieceAmount = gameState.PiecesToString(gameState.Player1PieceAmount),
            Player2PieceAmount = gameState.PiecesToString(gameState.Player2PieceAmount),
            GridHeight = gameState.SlidingGrid.GridHeight,
            GridWidth = gameState.SlidingGrid.GridWidth,
            GridCenterX = gameState.SlidingGrid.GridCenterX,
            GridCenterY = gameState.SlidingGrid.GridCenterY,
            StartRow = gameState.SlidingGrid.StartRow,
            EndRow = gameState.SlidingGrid.EndRow,
            StartColumn = gameState.SlidingGrid.StartCol,
            EndColumn = gameState.SlidingGrid.EndCol,
            ConfigurationId = GetConfigurationId(gameState.GameConfiguration.Name)
            
        });
        
        _context.SaveChanges();
        
    }

    public GameState GetGameByName(string gameName)
    {
        var game = _context.SaveGames
            .FirstOrDefault(c => c.Name == gameName);
        SlidingGrid slidingGrid = new SlidingGrid(game.GridCenterX, game.GridCenterY,
            game.StartRow, game.EndRow, game.StartColumn, game.EndColumn);

        EGamePiece[][]? gameBoard = JsonSerializer.Deserialize<EGamePiece[][]>(game.GameBoard);

        Configuration conf = GetConfigurationById(game.ConfigurationId);

        GameConfiguration gameConfiguration = new GameConfiguration()
        {
            Name = conf.Name,
            BoardSizeWidth = conf.BoardWidth,
            BoardSizeHeight = conf.BoardHeight,
            GridSizeWidth = conf.GridWidth,
            GridSizeHeight = conf.GridHeight,
            WinCondition = conf.WinCondition,
            MovePieceAfterNMoves = conf.MovePieceAfterNMoves,
            Player1Pieces = JsonSerializer.Deserialize<List<EGamePiece>>(conf.Player1Pieces),
            Player2Pieces = JsonSerializer.Deserialize<List<EGamePiece>>(conf.Player2Pieces)
        };
        
        GameState gameState = new GameState(gameBoard, gameConfiguration, slidingGrid);

        return gameState;
    }

    private int GetConfigurationId(string configurationName)
    {
        var configuration = _context.Configurations
            .FirstOrDefault(c => c.Name == configurationName);
        return configuration.Id;
    }

    private Configuration GetConfigurationById(int gameConfigurationId)
    {
        var configuration = _context.Configurations
            .FirstOrDefault(c => c.Id == gameConfigurationId);
        
        return configuration;
    }
    
}