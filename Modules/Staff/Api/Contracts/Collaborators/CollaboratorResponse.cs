namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;

public sealed record CollaboratorResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }


    public string Name { get; set; }
    public string Email { get; set; }


    public Guid MainBranchId { get; set; }
    public string MainBranchName { get; set; }


    public Guid MainPositionId { get; set; }
    public string MainPositionName { get; set; }


    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }
}