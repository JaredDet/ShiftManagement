using ShiftManagement.Api.Validations;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record ReplaceCollaboratorInShiftRequest
{
    [NotEmptyGuid]
    public Guid ShiftId { get; init; }

    [NotEmptyGuid]
    public Guid CurrentCollaboratorId { get; init; }

    [NotEmptyGuid]
    public Guid NewCollaboratorId { get; init; }
}