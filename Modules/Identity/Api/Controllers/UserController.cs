using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Identity.Api.Contracts.Users;
using ShiftManagement.Api.Modules.Identity.Application.Users;

namespace ShiftManagement.Api.Modules.Identity.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController(
    CreateUserUseCase createUserUseCase,
    UpdateUserUseCase updateUserUseCase,
    DeactivateUserUseCase deactivateUserUseCase
) : ControllerBase
{

    [HttpPost]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> CreateUser(
    [FromBody] CreateUserRequest request
)
    {
        var result = await createUserUseCase.ExecuteAsync(request);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "organization.company.not_found" => NotFound(result.Error),
            "identity.user.email_already_in_use" => Conflict(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> UpdateUser(
    Guid id,
    [FromBody] UpdateUserRequest request
    )
    {
        var result = await updateUserUseCase.ExecuteAsync(id, request);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "identity.user.not_found" => NotFound(result.Error),
            "identity.user.email_already_in_use" => Conflict(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpPatch("{id:guid}/deactivate")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> DeactivateUser(Guid id)
    {
        var result = await deactivateUserUseCase.ExecuteAsync(id);

        if (result.IsSuccess)
            return NoContent();

        return result.Error.Code switch
        {
            "identity.user.not_found" => NotFound(result.Error),
            _ => BadRequest(result.Error)
        };
    }
}