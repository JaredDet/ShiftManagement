namespace ShiftManagement.Api.BuildingBlocks.Exceptions;

public sealed class DomainException(
    string code,
    string message,
    ErrorType type
) : Exception(message)
{
    public string Code { get; } = code;
    public ErrorType Type { get; } = type;
}