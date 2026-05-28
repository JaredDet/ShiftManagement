using System.ComponentModel.DataAnnotations;
using ShiftManagement.Api.Validations;

namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Reviews;

public sealed record CancelClaimRequest
{
    [NotEmptyGuid]
    public Guid ActorId { get; init; }

    [MaxLength(500)]
    public string? Reason { get; init; }
}