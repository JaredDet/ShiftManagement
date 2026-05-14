using System.ComponentModel.DataAnnotations;

namespace ShiftManagement.Api.Shared;

public sealed class NotEmptyGuidAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is Guid g && g != Guid.Empty;
    }
}