namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.PositionAssignments;

public sealed record CollaboratorPositionsResponse
{
    public List<CollaboratorPositionResponse> Positions { get; set; } = new();
}