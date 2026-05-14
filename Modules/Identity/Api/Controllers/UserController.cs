using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Identity.Api.Contracts;
using ShiftManagement.Api.Modules.Identity.Application;

namespace ShiftManagement.Api.Modules.Identity.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(
    CreateUserUseCase createUserUseCase
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser(
    [FromBody] CreateUserRequest request
)
    {
        var result = await createUserUseCase
            .ExecuteAsync(request);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}