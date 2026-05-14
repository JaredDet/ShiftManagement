using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Identity.Api.Contracts.Users;
using ShiftManagement.Api.Modules.Identity.Application.Users;

namespace ShiftManagement.Api.Modules.Identity.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(
    CreateUserUseCase createUserUseCase,
    UpdateUserUseCase updateUserUseCase,
    DeactivateUserUseCase deactivateUserUseCase
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserRequest request
    )
    {
        var result = await createUserUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser(
        Guid id,
        [FromBody] UpdateUserRequest request
    )
    {
        var result = await updateUserUseCase.ExecuteAsync(id, request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeactivateUser(Guid id)
    {
        var result = await deactivateUserUseCase.ExecuteAsync(id);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return NoContent();
    }
}