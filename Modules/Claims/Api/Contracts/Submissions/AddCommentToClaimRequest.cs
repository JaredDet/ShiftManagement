namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;

public sealed class AddCommentToClaimRequest
{
    public Guid ClaimId { get; init; }

    public string Content { get; init; } = string.Empty;
}