namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;

public sealed record CollaboratorResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }


    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;


    public Guid MainBranchId { get; set; }
    public string MainBranchName { get; set; } = string.Empty;


    public Guid MainPositionId { get; set; }
    public string MainPositionName { get; set; } = string.Empty;


    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}