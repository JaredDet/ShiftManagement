namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Reviews;

public sealed class AssignClaimRequest
{
    public Guid ClaimId { get; init; }

    public Guid AssignedToUserId { get; init; }
}