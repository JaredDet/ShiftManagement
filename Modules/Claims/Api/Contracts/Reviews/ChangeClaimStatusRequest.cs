namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Reviews;

public sealed class ChangeClaimStatusRequest
{
    public Guid ClaimId { get; init; }

    public string Status { get; init; } = string.Empty;
}