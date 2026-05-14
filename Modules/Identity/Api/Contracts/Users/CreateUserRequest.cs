using System.ComponentModel.DataAnnotations;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Identity.Api.Contracts.Users;

public sealed record CreateUserRequest
{
    [NotEmptyGuid]
    public Guid CompanyId { get; init; }

    [Required]
    [MinLength(6)]
    [MaxLength(100)]
    public required string Name { get; init; }

    [Required]
    [EmailAddress]
    public required string Email { get; init; }

    [Required]
    [StrongPassword]
    public required string Password { get; init; }
}