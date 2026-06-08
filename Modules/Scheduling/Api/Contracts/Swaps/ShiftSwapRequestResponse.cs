namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record ShiftSwapRequestResponse
{
    public Guid Id { get; init; }

    public Guid RequesterId { get; init; }
    public string RequesterName { get; init; } = string.Empty;

    public Guid TargetCollaboratorId { get; init; }
    public string TargetCollaboratorName { get; init; } = string.Empty;

    public Guid SourceShiftId { get; init; }
    public Guid TargetShiftId { get; init; }

    public string Status { get; init; } = string.Empty;

    public DateTime CreatedAt { get; init; }
    public DateTime? RespondedAt { get; init; }
    public DateTime? ApprovedAt { get; init; }
}