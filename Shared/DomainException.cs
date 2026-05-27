namespace ShiftManagement.Api.Shared;

public sealed class DomainException(
    string code,
    string message,
    ErrorType type
) : Exception(message)
{
    public string Code { get; } = code;
    public ErrorType Type { get; } = type;
}