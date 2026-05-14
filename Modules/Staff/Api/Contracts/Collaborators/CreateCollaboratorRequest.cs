using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;

public sealed record CreateCollaboratorRequest
{
    [NotEmptyGuid]
    public Guid UserId { get; set; }
    [NotEmptyGuid]
    public Guid CompanyId { get; set; }
    [NotEmptyGuid]
    public Guid MainBranchId { get; set; }
    [NotEmptyGuid]
    public Guid MainPositionId { get; set; }
}