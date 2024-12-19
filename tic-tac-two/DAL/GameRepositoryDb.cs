using System.Text.Json;
using Domain;
using GameBrain;

namespace DAL;

public class GameRepositoryDb(AppDbContext context) : IGameRepository
{
    public Dictionary<int, string> GetGameNames()
    {
        return context.SaveGames
            .OrderBy(c => c.Name)
            .ToDictionary(c => c.Id, c => c.Name);
    }

    public void SaveGame(GameState gameState)
    {
        var existingGame = context.SaveGames.FirstOrDefault(g => g.Id == gameState.Id);

        if (existingGame != null)
        {
            existingGame.Name = gameState.Name;
            existingGame.GameBoard = gameState.BoardToString();
            existingGame.NextMoveBy = gameState.NextMoveBy;
            existingGame.Player1PieceAmount = gameState.Player1PieceAmount;
            existingGame.Player2PieceAmount = gameState.Player2PieceAmount;
            existingGame.GridHeight = gameState.SlidingGrid.GridHeight;
            existingGame.GridWidth = gameState.SlidingGrid.GridWidth;
            existingGame.GridCenterX = gameState.SlidingGrid.GridCenterX;
            existingGame.GridCenterY = gameState.SlidingGrid.GridCenterY;
            existingGame.StartRow = gameState.SlidingGrid.StartRow;
            existingGame.EndRow = gameState.SlidingGrid.EndRow;
            existingGame.StartColumn = gameState.SlidingGrid.StartCol;
            existingGame.EndColumn = gameState.SlidingGrid.EndCol;
            existingGame.GamePassword = gameState.GamePassword;
            existingGame.MadeMoves = gameState.MadeMoves;
            existingGame.ConfigurationId = gameState.GameConfiguration.Id;
            existingGame.GameTypeId = gameState.GameType;
            
            context.SaveChanges();
        }
        else
        {
            var saveGame = new SaveGame
            {
                Name = gameState.Name,
                GameBoard = gameState.BoardToString(),
                NextMoveBy = gameState.NextMoveBy,
                Player1PieceAmount = gameState.Player1PieceAmount,
                Player2PieceAmount = gameState.Player2PieceAmount,
                GridHeight = gameState.SlidingGrid.GridHeight,
                GridWidth = gameState.SlidingGrid.GridWidth,
                GridCenterX = gameState.SlidingGrid.GridCenterX,
                GridCenterY = gameState.SlidingGrid.GridCenterY,
                StartRow = gameState.SlidingGrid.StartRow,
                EndRow = gameState.SlidingGrid.EndRow,
                StartColumn = gameState.SlidingGrid.StartCol,
                EndColumn = gameState.SlidingGrid.EndCol,
                GamePassword = gameState.GamePassword,
                MadeMoves = gameState.MadeMoves,
                ConfigurationId = gameState.GameConfiguration.Id,
                GameTypeId = gameState.GameType
            };
            
            context.SaveGames.Add(saveGame);
            context.SaveChanges();
            gameState.Id = saveGame.Id;
        }
    
    }

    public GameState GetGameStateById(int gameId)
    {
        var game = context.SaveGames
            .FirstOrDefault(c => c.Id == gameId);
        
        var slidingGrid = new SlidingGrid(game!.GridCenterX, game.GridCenterY,
            game.StartRow, game.EndRow, game.StartColumn, game.EndColumn, game.GridHeight, game.GridWidth);

        var gameBoard = JsonSerializer.Deserialize<EGamePiece[][]>(game.GameBoard);

        var conf = GetConfigurationById(game.ConfigurationId);

        var gameConfiguration = new GameConfiguration()
        {
            Id = conf!.Id,
            Name = conf.Name,
            BoardWidth = conf.BoardWidth,
            BoardHeight = conf.BoardHeight,
            GridWidth = conf.GridWidth,
            GridHeight = conf.GridHeight,
            WinCondition = conf.WinCondition,
            MovePieceAfterNMoves = conf.MovePieceAfterNMoves,
            Player1PieceAmount = game.Player2PieceAmount,
            Player2PieceAmount = game.Player2PieceAmount
        };
        
        var gameState = new GameState(gameBoard!, gameConfiguration, slidingGrid, game.MadeMoves, game.GamePassword, game.NextMoveBy,
            game.Id, game.GameTypeId, game.Player1PieceAmount, game.Player2PieceAmount)
        {
            Name = game.Name
        };

        return gameState;
    }

    public SaveGame GetSaveGameById(int gameId)
    {
        var game = context.SaveGames
            .FirstOrDefault(c => c.Id == gameId);
        return game!;
    }

    public void DeleteGame(int gameId)
    {
        var gameToDelete = context.SaveGames.FirstOrDefault(g => g.Id == gameId);
    
        if (gameToDelete == null)
        {
            return;
        }
        
        context.SaveGames.Remove(gameToDelete);
        
        context.SaveChanges();
    }

    private Configuration? GetConfigurationById(int gameConfigurationId)
    {
        var configuration = context.Configurations
            .FirstOrDefault(c => c.Id == gameConfigurationId);
        
        return configuration;
    }
}