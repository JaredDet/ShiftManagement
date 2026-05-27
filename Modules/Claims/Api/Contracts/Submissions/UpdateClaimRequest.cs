using System.ComponentModel.DataAnnotations;
using ShiftManagement.Api.Modules.Claims.Domain;

namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;

public sealed record UpdateClaimRequest
{
    [Required]
    [MinLength(5)]
    [MaxLength(150)]
    public string Title { get; init; } = string.Empty;

    [Required]
    [MinLength(10)]
    [MaxLength(5000)]
    public string Description { get; init; } = string.Empty;

    [EnumDataType(typeof(ClaimPriority))]
    public ClaimPriority Priority { get; init; }
}