namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Reviews;

public sealed class ResolveClaimRequest
{
    public Guid ClaimId { get; init; }

    public string ResolutionComment { get; init; } = string.Empty;
}