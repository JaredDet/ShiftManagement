namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Reviews;

public sealed class RejectClaimRequest
{
    public Guid ClaimId { get; init; }

    public string RejectionReason { get; init; } = string.Empty;
}