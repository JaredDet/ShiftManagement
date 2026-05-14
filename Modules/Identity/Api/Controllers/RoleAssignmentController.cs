using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Identity.Api.Contracts.RoleAssignments;
using ShiftManagement.Api.Modules.Identity.Application.RoleAssignments;

namespace ShiftManagement.Api.Modules.Identity.Api.Controllers;

[ApiController]
[Route("api/users/{userId:guid}/roles")]
public class RoleAssignmentController(
    AssignRoleToUserUseCase assignRoleToUserUseCase,
    RemoveRoleFromUserUseCase removeRoleFromUserUseCase,
    ListUserRolesUseCase listUserRolesUseCase
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserRoles(Guid userId)
    {
        var result = await listUserRolesUseCase.ExecuteAsync(userId);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRole(
        Guid userId,
        [FromBody] AssignRoleToUserRequest request
    )
    {
        var result = await assignRoleToUserUseCase.ExecuteAsync(userId, request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveRole(
        Guid userId,
        [FromBody] RemoveRoleFromUserRequest request
    )
    {
        var result = await removeRoleFromUserUseCase.ExecuteAsync(userId, request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }
}