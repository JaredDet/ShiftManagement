using System.ComponentModel.DataAnnotations;

namespace ShiftManagement.Api.Modules.Organization.Api.Contracts;

public sealed record CreateBranchRequest(
    [Required]
    Guid CompanyId,

    [Required]
    [MaxLength(255)]
    string Name,

    [Required]
    [MaxLength(500)]
    string Address
);