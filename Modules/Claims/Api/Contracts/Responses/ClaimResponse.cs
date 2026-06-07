namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;

public sealed record ClaimResponse
{
    public Guid Id { get; init; }

    public Guid CompanyId { get; init; }

    public Guid CollaboratorId { get; init; }

    public string CollaboratorName { get; init; } = string.Empty;

    public string Reason { get; init; } = string.Empty;

    public string Priority { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public Guid? AssignedToUserId { get; init; }

    public string? AssignedToUserName { get; init; }

    public int CommentsCount { get; init; }

    public int EvidencesCount { get; init; }

    public bool IsClosed { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }

    public DateTime? ResolvedAt { get; init; }

    public DateTime? CancelledAt { get; init; }
}