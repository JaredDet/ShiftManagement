using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Session.Application;

namespace ShiftManagement.Api.Modules.Session.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/session")]
public class SessionController(
    GetSessionUseCase getSession
) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        return (await getSession.ExecuteAsync())
            .Match(Ok);
    }
}