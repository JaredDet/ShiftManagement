using System.ComponentModel.DataAnnotations;
using ShiftManagement.Api.Modules.Claims.Domain;
using ShiftManagement.Api.Validations;

namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;

public sealed record CreateClaimRequest
{
    [NotEmptyGuid]
    public Guid CollaboratorId { get; init; }

    [NotEmptyGuid]
    public Guid CompanyId { get; init; }

    [EnumDataType(typeof(ClaimReason))]
    public ClaimReason Reason { get; init; }

    [Required]
    [MinLength(5)]
    [MaxLength(150)]
    public string Title { get; init; } = string.Empty;

    [Required]
    [MinLength(10)]
    [MaxLength(5000)]
    public string Description { get; init; } = string.Empty;

    [EnumDataType(typeof(ClaimPriority))]
    public ClaimPriority Priority { get; init; }
}