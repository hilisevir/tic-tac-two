using System.Text.Json;

namespace GameBrain;

public class GameState
{

    public int Id {get;set;}
    public string Name { get; set; } = default!;
    public EGamePiece[][] GameBoard { get; set; } = default!;
    public EGamePiece NextMoveBy { get; set; }
    public string GamePassword { get; set; }
    public GameConfiguration GameConfiguration {get; set;}
    public SlidingGrid SlidingGrid { get; set; }
    public int Player1PieceAmount { get; set; }
    public int Player2PieceAmount { get; set; }
    public int GameType { get; set; }
    public int MadeMoves { get; set; }
    
    
    public GameState(EGamePiece[][] gameBoard,
                    GameConfiguration gameConfiguration, 
                    SlidingGrid slidingGrid, int madeMoves = -100,
                    string gamePassword = "", 
                    EGamePiece nextMoveBy = EGamePiece.X, 
                    int id = -1, int gameType = -1, 
                    int player1PieceAmount = -1,
                    int player2PieceAmount = -1,
                    string name = "")
    {
        Id = id;
        Name = name;
        SlidingGrid = slidingGrid;
        GameBoard = gameBoard;
        GamePassword = string.IsNullOrEmpty(gamePassword) ? GeneratePassword() : gamePassword;
        GameConfiguration = gameConfiguration;
        Player1PieceAmount = player1PieceAmount > -1 ? player1PieceAmount : gameConfiguration.Player1PieceAmount;
        Player2PieceAmount = player2PieceAmount > -1 ? player2PieceAmount : gameConfiguration.Player2PieceAmount;
        slidingGrid.GameConfiguration = gameConfiguration;
        MadeMoves = madeMoves > -100 ? madeMoves : gameConfiguration.MovePieceAfterNMoves;
        GameType = gameType;
        NextMoveBy = nextMoveBy;
    }
    
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
    
    public string BoardToString()
    {
        return JsonSerializer.Serialize(GameBoard);
    }
    
    private static string GeneratePassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 8)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}