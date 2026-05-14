namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;

public sealed record CollaboratorListResponse
{
    public List<CollaboratorResponse> Collaborators { get; set; } = [];
}