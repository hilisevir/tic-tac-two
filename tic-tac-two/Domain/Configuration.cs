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
    
    public string Player1Pieces { get; set; }
    public string Player2Pieces { get; set; }
    
    
    public ICollection<SaveGame>? SaveGames { get; set; }
    
    public override string ToString() => Name + $"(Board size: {BoardWidth}x{BoardHeight}, " +
                                         $"grid size: {GridWidth}x{GridHeight}, to win: {WinCondition}, " +
                                         $"can move pieces after: {MovePieceAfterNMoves})" +
                                         $"({SaveGames?.Count.ToString() ?? "not joined"})";
}