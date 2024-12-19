using System.ComponentModel.DataAnnotations;

namespace Domain;

public class GameType : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    [MaxLength(1280)]
    public string? Description { get; set; }
}