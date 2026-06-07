namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.BranchAssignments;

public sealed record CollaboratorBranchResponse
{
    public Guid CollaboratorId { get; set; }

    public Guid BranchId { get; set; }
    public string BranchName { get; set; }

    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }
}