using System.ComponentModel.DataAnnotations;

namespace ShiftManagement.Api.Shared;

public sealed class StrongPasswordAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        if (value is not string password || string.IsNullOrWhiteSpace(password))
            return new ValidationResult("Password is required");

        bool hasLower = password.Any(char.IsLower);
        bool hasUpper = password.Any(char.IsUpper);
        bool hasDigit = password.Any(char.IsDigit);
        bool hasSpecial = password.Any(c => !char.IsLetterOrDigit(c));

        if (password.Length < 8 || !hasLower || !hasUpper || !hasDigit || !hasSpecial)
        {
            return new ValidationResult(
                "Password must be at least 8 characters and contain uppercase, lowercase, number and special character"
            );
        }

        return ValidationResult.Success;
    }
}