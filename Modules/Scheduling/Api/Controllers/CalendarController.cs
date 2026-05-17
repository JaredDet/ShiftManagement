using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Application.Calendar;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/calendar")]
public class CalendarController(
    GetCalendarUseCase useCase
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] CalendarRequest request)
    {
        var result = await useCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}