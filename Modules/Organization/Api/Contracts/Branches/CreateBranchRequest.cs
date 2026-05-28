using System.ComponentModel.DataAnnotations;
using ShiftManagement.Api.Validations;

namespace ShiftManagement.Api.Modules.Organization.Api.Contracts.Branches;

public sealed record CreateBranchRequest(
    [NotEmptyGuid]
    Guid CompanyId,

    [Required]
    [MaxLength(255)]
    string Name,

    [Required]
    [MaxLength(500)]
    string Address
);