namespace GameBrain;

public record struct GameConfiguration()
{
    public int Id {get; set;}
    public string Name { get; set; } = default!;
    
    public int BoardWidth { get; set; } = 5;
    public int BoardHeight { get; set; } = 5;
    
    public int GridWidth { get; set; } = 3;
    public int GridHeight { get; set; } = 3;
    
    public int WinCondition { get; set; } = 3;
    public int MovePieceAfterNMoves { get; set; } = 2;

    public int Player1PieceAmount { get; set; } = 4;
    public int Player2PieceAmount { get; set; } = 4;

    public override string ToString() => Name + $"(Board size: {BoardWidth}x{BoardHeight}, " +
                                         $"grid size: {GridWidth}x{GridHeight}, to win: {WinCondition}, " +
                                         $"can move pieces after: {MovePieceAfterNMoves})";
}