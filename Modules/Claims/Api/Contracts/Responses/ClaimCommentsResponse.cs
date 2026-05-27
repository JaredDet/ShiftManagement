namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;

public sealed class ClaimCommentsResponse
{
    public Guid ClaimId { get; init; }

    public IReadOnlyList<ClaimCommentResponse> Comments { get; init; } = [];
}