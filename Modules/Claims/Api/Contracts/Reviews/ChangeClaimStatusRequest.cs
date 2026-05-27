using ShiftManagement.Api.Modules.Claims.Domain;

namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Reviews;

public sealed class ChangeClaimStatusRequest
{
    public Guid ClaimId { get; init; }

    public ClaimStatus Status { get; init; }
}