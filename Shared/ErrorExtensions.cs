using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Shared;

public static class ErrorExtensions
{
    public static IActionResult ToActionResult(
        this Error error)
    {
        return error.Type switch
        {
            ErrorType.NotFound =>
                new NotFoundObjectResult(error),

            ErrorType.Validation =>
                new BadRequestObjectResult(error),

            ErrorType.Conflict =>
                new ConflictObjectResult(error),

            ErrorType.Unauthorized =>
                new UnauthorizedObjectResult(error),

            ErrorType.Forbidden =>
                new ForbidResult(),

            _ =>
                new ObjectResult(error)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                }
        };
    }
}