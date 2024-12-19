using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Configuration : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    public int BoardWidth { get; set; }
    public int BoardHeight { get; set; }
    
    public int GridWidth { get; set; }
    public int GridHeight { get; set; }
    
    public int WinCondition { get; set; }
    
    public int MovePieceAfterNMoves { get; set; }
    
    public int Player1PieceAmount { get; set; }
    public int Player2PieceAmount { get; set; }
    
    
    public override string ToString() => Name + $"(Board size: {BoardWidth}x{BoardHeight}, " +
                                         $"grid size: {GridWidth}x{GridHeight}, to win: {WinCondition}, " +
                                         $"can move pieces after: {MovePieceAfterNMoves})";
}