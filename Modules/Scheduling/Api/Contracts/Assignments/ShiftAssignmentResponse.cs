namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record ShiftAssignmentResponse
{
    public Guid Id { get; init; }
    public Guid ShiftId { get; init; }

    public Guid CollaboratorId { get; init; }
    public string CollaboratorName { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public DateTime CreatedAt { get; init; }
    public DateTime? CancelledAt { get; init; }
}