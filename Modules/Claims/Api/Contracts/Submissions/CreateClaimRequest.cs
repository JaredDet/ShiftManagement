using ShiftManagement.Api.Modules.Claims.Domain;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;

public sealed class CreateClaimRequest
{
    [NotEmptyGuid]
    public Guid CollaboratorId { get; init; }
    [NotEmptyGuid]
    public Guid CompanyId { get; set; }

    public ClaimReason Reason { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public ClaimPriority Priority { get; init; }
}