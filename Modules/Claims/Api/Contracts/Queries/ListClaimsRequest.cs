using System.ComponentModel.DataAnnotations;

using ShiftManagement.Api.Modules.Claims.Domain;

namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Queries;

public sealed record ListClaimsRequest
{
    public Guid? CollaboratorId { get; init; }

    [EnumDataType(typeof(ClaimStatus))]
    public ClaimStatus? Status { get; init; }

    [EnumDataType(typeof(ClaimReason))]
    public ClaimReason? Reason { get; init; }

    [EnumDataType(typeof(ClaimPriority))]
    public ClaimPriority? Priority { get; init; }

    public Guid? AssignedToUserId { get; init; }

    public DateTime? CreatedFrom { get; init; }

    public DateTime? CreatedTo { get; init; }
}