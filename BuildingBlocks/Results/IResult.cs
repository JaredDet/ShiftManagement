using ShiftManagement.Api.BuildingBlocks.Exceptions;

namespace ShiftManagement.Api.BuildingBlocks.Results;

public interface IResult
{
    bool IsSuccess { get; }

    Error? Error { get; }
}