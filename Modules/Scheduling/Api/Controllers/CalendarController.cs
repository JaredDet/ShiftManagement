using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Application.Calendar;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/calendar")]
public class CalendarController(
    GetCalendarUseCase get
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] CalendarRequest request)
    {
        return (await get.ExecuteAsync(request))
            .Match(Ok);
    }
}