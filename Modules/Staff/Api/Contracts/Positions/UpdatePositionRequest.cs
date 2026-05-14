using System.ComponentModel.DataAnnotations;

namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;

public sealed record UpdatePositionRequest
{
    [Required]
    [MinLength(2)]
    [MaxLength(255)]
    public string Name { get; init; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }
}