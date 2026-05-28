namespace ShiftManagement.Api.BuildingBlocks.Exceptions;

public sealed record Error(
    string Code,
    string Message,
    ErrorType Type
)
{
    public int HttpStatus => Type switch
    {
        ErrorType.Validation => 400,
        ErrorType.NotFound => 404,
        ErrorType.Conflict => 409,
        ErrorType.Unauthorized => 401,
        ErrorType.Forbidden => 403,
        _ => 500
    };
}