using System.ComponentModel.DataAnnotations;
using ShiftManagement.Api.Validations;

namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Reviews;

public sealed record ResolveClaimRequest
{
    [NotEmptyGuid]
    public Guid ReviewerId { get; init; }

    [Required]
    [MaxLength(500)]
    public string ResolutionComment { get; init; } = string.Empty;
}