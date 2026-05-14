using System.ComponentModel.DataAnnotations;

namespace ShiftManagement.Api.Modules.Organization.Api.Contracts.Companies;

public sealed record CreateCompanyRequest(
    [Required]
    [MaxLength(255)]
    string Name
);