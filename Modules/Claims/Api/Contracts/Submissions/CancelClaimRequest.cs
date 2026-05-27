namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;

public sealed class CancelClaimRequest
{
    public Guid ClaimId { get; init; }
}