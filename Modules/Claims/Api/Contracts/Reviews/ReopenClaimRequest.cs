using System.ComponentModel.DataAnnotations;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Reviews;

public sealed record ReopenClaimRequest
{
    [NotEmptyGuid]
    public Guid ActorId { get; init; }

    [Required]
    [MaxLength(500)]
    public string Reason { get; init; } = string.Empty;
}