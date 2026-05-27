using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Identity.Api.Contracts.RoleAssignments;
using ShiftManagement.Api.Modules.Identity.Application.RoleAssignments;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Identity.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/users/{userId:guid}/roles")]
public class RoleAssignmentController(
    AssignRoleToUserUseCase assign,
    RemoveRoleFromUserUseCase remove,
    ListUserRolesUseCase list
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserRoles(Guid userId)
    {
        return (await list.ExecuteAsync(userId))
            .Match(Ok);
    }

    [HttpPost]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> AssignRole(
    Guid userId,
    [FromBody] AssignRoleToUserRequest request
    )
    {
        return (await assign.ExecuteAsync(userId, request))
            .Match(Ok);
    }

    [HttpDelete]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> RemoveRole(
    Guid userId,
    [FromBody] RemoveRoleFromUserRequest request
)
    {
        return (await remove.ExecuteAsync(userId, request))
            .Match(NoContent);
    }
}