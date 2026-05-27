namespace ShiftManagement.Api.Shared;

public interface IResult
{
    bool IsSuccess { get; }

    Error? Error { get; }
}