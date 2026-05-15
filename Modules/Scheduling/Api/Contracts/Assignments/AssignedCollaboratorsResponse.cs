namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record AssignedCollaboratorsResponse
{
    public Guid ShiftId { get; init; }
    public List<ShiftAssignmentResponse> Collaborators { get; init; }
}