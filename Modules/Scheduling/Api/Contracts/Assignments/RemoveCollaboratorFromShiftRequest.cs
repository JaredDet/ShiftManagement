namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record RemoveCollaboratorFromShiftRequest
{
    public Guid ShiftId { get; init; }
    public Guid CollaboratorId { get; init; }
}