using System.Text.Json;

namespace GameBrain;

public class GameState
{
    
    public string Name { get; set; }
    public EGamePiece[][] GameBoard { get; set; }

    public EGamePiece NextMoveBy { get; set; } = EGamePiece.X;

    public GameConfiguration GameConfiguration {get; set;}
    public SlidingGrid SlidingGrid { get; set; }
    public List<EGamePiece>? Player1PieceAmount { get; set; }
    public List<EGamePiece>? Player2PieceAmount { get; set; }

    public GameState(EGamePiece[][] gameBoard, GameConfiguration gameConfiguration, SlidingGrid slidingGrid)
    {
        SlidingGrid = slidingGrid;
        GameBoard = gameBoard;
        GameConfiguration = gameConfiguration;
        Player1PieceAmount = gameConfiguration.Player1Pieces;
        Player2PieceAmount = gameConfiguration.Player2Pieces;
        slidingGrid._gameConfiguration = gameConfiguration;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
    
    public string BoardToString()
    {
        return JsonSerializer.Serialize(GameBoard);
    }
    
    public string PiecesToString(List<EGamePiece>? pieces)
    {
        return JsonSerializer.Serialize(pieces);
    }
}