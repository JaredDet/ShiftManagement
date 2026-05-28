using System.ComponentModel.DataAnnotations;
using ShiftManagement.Api.Validations;

namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;

public sealed record CreatePositionRequest
{
    [NotEmptyGuid]
    public Guid CompanyId { get; init; }

    [Required]
    [MinLength(2)]
    [MaxLength(255)]
    public string Name { get; init; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; init; }
}