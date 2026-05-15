namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record RequestShiftSwapRequest
{
    public Guid SourceShiftId { get; init; }
    public Guid TargetShiftId { get; init; }
    public Guid TargetCollaboratorId { get; init; }
}