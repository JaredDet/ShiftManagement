using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Reviews;

public sealed record AssignClaimRequest
{
    [NotEmptyGuid]
    public Guid AssignedToUserId { get; init; }
}