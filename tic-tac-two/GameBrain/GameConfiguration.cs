using System.Text.Json;

namespace GameBrain;

public record struct GameConfiguration()
{
    public string Name { get; set; } = default!;
    
    public int BoardSizeWidth { get; set; } = 5;
    public int BoardSizeHeight { get; set; } = 5;
    
    public int GridSizeWidth { get; set; } = 3;
    public int GridSizeHeight { get; set; } = 3;
    
    // public string GridColor { get; set; } = "Red";
    
    public int WinCondition { get; set; } = 3;
    public int MovePieceAfterNMoves { get; set; } = 2;
    
    public List<EGamePiece>? Player1Pieces { get; set; } = [EGamePiece.X, EGamePiece.X, EGamePiece.X, EGamePiece.X];
    public List<EGamePiece>? Player2Pieces { get; set; } = [EGamePiece.O, EGamePiece.O, EGamePiece.O, EGamePiece.O];

    public override string ToString() => Name + $"(Board size: {BoardSizeWidth}x{BoardSizeHeight}, " +
                                         $"grid size: {GridSizeWidth}x{GridSizeHeight}, to win: {WinCondition}, " +
                                         $"can move pieces after: {MovePieceAfterNMoves})";

    public string PiecesToString(List<EGamePiece>? pieces)
    {
        return JsonSerializer.Serialize(pieces);
    }
}