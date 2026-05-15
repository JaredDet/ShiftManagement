namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record ReplaceCollaboratorInShiftRequest
{
    public Guid ShiftId { get; init; }
    public Guid CurrentCollaboratorId { get; init; }
    public Guid NewCollaboratorId { get; init; }
}