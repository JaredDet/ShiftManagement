using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Controllers;

[ApiController]
[Route("api/calendar")]
public class CalendarController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> Get([FromQuery] CalendarRequest request)
    {
        throw new NotImplementedException();
    }
}