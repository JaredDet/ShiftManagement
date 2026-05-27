namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;

public sealed record ClaimCommentsResponse
{
    public Guid ClaimId { get; init; }

    public IReadOnlyList<ClaimCommentResponse> Comments { get; init; } = [];
}