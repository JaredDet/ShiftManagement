using ShiftManagement.Api.Validations;

namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Reviews;

public sealed record AssignClaimRequest
{
    [NotEmptyGuid]
    public Guid AssignedToUserId { get; init; }
}