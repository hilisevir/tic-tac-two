using System.ComponentModel.DataAnnotations;
using GameBrain;

namespace Domain;

public class SaveGame : BaseEntity
{
    [MaxLength(128)] 
    public string Name { get; set; } = default!;
    [MaxLength(10240)]
    public string GameBoard { get; set; } = default!;
    [MaxLength(1024)]
    public string GamePassword { get; set; } = default!;
    public EGamePiece NextMoveBy { get; set; }
    public int Player1PieceAmount { get; set; }
    public int Player2PieceAmount { get; set; }
    public int GridHeight { get; set; }
    public int GridWidth { get; set; }
    public int GridCenterX { get; set; }
    public int GridCenterY { get; set; }
    public int StartRow { get; set; }
    public int EndRow { get; set; }
    public int StartColumn { get; set; }
    public int EndColumn { get; set; }
    
    public int MadeMoves { get; set; }
    
    // Expose the Foreign Key
    public int ConfigurationId { get; set; }
    public Configuration? Configuration { get; set; }
    
}