using System.ComponentModel.DataAnnotations;

namespace ShiftManagement.Api.Modules.Organization.Api.Contracts;

public sealed record CreateCompanyRequest(
    [Required]
    [MaxLength(255)]
    string Name
);