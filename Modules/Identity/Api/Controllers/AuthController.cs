using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Identity.Api.Contracts.Auth;
using ShiftManagement.Api.Modules.Identity.Application;

namespace ShiftManagement.Api.Modules.Identity.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(LoginUseCase login) : ControllerBase
{

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        return (await login.ExecuteAsync(request))
            .Match(Ok);
    }
}