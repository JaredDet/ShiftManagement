using System.ComponentModel.DataAnnotations;

namespace ShiftManagement.Api.Shared;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class DateRangeAttribute : ValidationAttribute
{
    private readonly string _startPropertyName;

    public DateRangeAttribute(string startPropertyName)
    {
        _startPropertyName = startPropertyName;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not DateTime endDate)
            return ValidationResult.Success;

        var startProperty = validationContext.ObjectType.GetProperty(_startPropertyName);

        if (startProperty is null)
            return new ValidationResult($"Unknown property: {_startPropertyName}");

        var startValue = startProperty.GetValue(validationContext.ObjectInstance);

        if (startValue is not DateTime startDate)
            return ValidationResult.Success;

        if (startDate >= endDate)
        {
            return new ValidationResult(
                "Start date must be earlier than end date"
            );
        }

        return ValidationResult.Success;
    }
}