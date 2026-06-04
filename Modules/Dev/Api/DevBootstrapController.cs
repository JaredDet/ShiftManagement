using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Dev.Application;

namespace ShiftManagement.Api.Modules.Dev.Api;

[ApiController]
[Route("api/dev/bootstrap")]
public class DevBootstrapController(DevBootstrapUseCase bootstrap, IHostEnvironment env)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Bootstrap(BootstrapCompanyRequest request)
    {
        if (!env.IsDevelopment())
            return Forbid();

        return (await bootstrap.ExecuteAsync(request))
            .Match(value =>
                StatusCode(
                    StatusCodes.Status201Created,
                    value
                )
            );
    }
}