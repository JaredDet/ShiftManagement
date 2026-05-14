using System.ComponentModel.DataAnnotations;

namespace ShiftManagement.Api.Modules.Identity.Api.Contracts.Users;

public sealed record UpdateUserRequest
{
    [Required]
    [MinLength(6)]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}