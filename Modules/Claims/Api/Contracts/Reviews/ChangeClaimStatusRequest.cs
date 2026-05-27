using System.ComponentModel.DataAnnotations;
using ShiftManagement.Api.Modules.Claims.Domain;

namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Reviews;

public sealed record ChangeClaimStatusRequest
{
    [EnumDataType(typeof(ClaimStatus))]
    public ClaimStatus Status { get; init; }
}