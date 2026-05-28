using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Identity.Api.Contracts.Users;
using ShiftManagement.Api.Modules.Identity.Application.Users;

namespace ShiftManagement.Api.Modules.Identity.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController(
    CreateUserUseCase create,
    UpdateUserUseCase update,
    DeactivateUserUseCase deactivate
) : ControllerBase
{

    [HttpPost]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserRequest request)
    {
        return (await create.ExecuteAsync(request))
            .Match(Ok);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> UpdateUser(
    Guid id,
    [FromBody] UpdateUserRequest request
    )
    {
        return (await update.ExecuteAsync(id, request))
            .Match(Ok);
    }

    [HttpPatch("{id:guid}/deactivate")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> DeactivateUser(Guid id)
    {
        return (await deactivate.ExecuteAsync(id))
            .Match(NoContent);
    }
}