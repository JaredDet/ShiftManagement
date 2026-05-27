namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;

public sealed class ClaimCommentResponse
{
    public Guid Id { get; init; }

    public Guid ClaimId { get; init; }

    public Guid AuthorUserId { get; init; }

    public string AuthorName { get; init; } = string.Empty;

    public string Content { get; init; } = string.Empty;

    public DateTime CreatedAt { get; init; }
}