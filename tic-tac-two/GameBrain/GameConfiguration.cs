namespace GameBrain;

public record struct GameConfiguration()
{
    public string Name { get; set; } = default!;
    
    public int BoardSizeWidth { get; set; } = 5;
    public int BoardSizeHeight { get; set; } = 5;
    
    public int GridSizeWidth { get; set; } = 3;
    public int GridSizeHeight { get; set; } = 3;
    
    public string GridColor { get; set; } = "Red";
    
    // how many
    public int WinCondition { get; set; } = 3;
    // 0 disabled
    public int MovePieceAfterNMoves { get; set; } = 0;

    public override string ToString() => $"Board size: {BoardSizeWidth}x{BoardSizeHeight}, " +
                                         $"grid size: {GridSizeWidth}x{GridSizeHeight}, to win: {WinCondition}, " + 
                                         $"grid color: {GridColor}" +
                                         $"can move pieces after: {MovePieceAfterNMoves}";
}