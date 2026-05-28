using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Application.Calendar;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/calendar")]
public class CalendarController(
    GetCalendarUseCase get,
    GetMyCalendarUseCase getMyCalendar
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] CalendarRequest request
    )
    {
        return (await get.ExecuteAsync(request))
            .Match(Ok);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyCalendar(
        [FromQuery] CalendarRequest request
    )
    {
        return (await getMyCalendar.ExecuteAsync(request))
            .Match(Ok);
    }
}