using System.ComponentModel.DataAnnotations;

namespace ShiftManagement.Api.Modules.Organization.Api.Contracts.Branches;

public sealed record UpdateBranchRequest(
    [Required]
    [MaxLength(255)]
    string Name,

    [Required]
    [MaxLength(500)]
    string Address
);