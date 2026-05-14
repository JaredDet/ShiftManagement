namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.PositionAssignments;

public sealed record CollaboratorPositionResponse
{
    public Guid CollaboratorId { get; set; }
    public Guid PositionId { get; set; }
    public string PositionName { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}