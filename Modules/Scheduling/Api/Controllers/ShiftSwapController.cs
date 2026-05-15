using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Controllers;

[ApiController]
[Route("api/shifts/swaps")]
public class ShiftSwapController : ControllerBase
{
    [HttpPost]
    public Task<IActionResult> Request(RequestShiftSwapRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPost("approve")]
    public Task<IActionResult> Approve(ApproveShiftSwapRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPost("reject")]
    public Task<IActionResult> Reject(RejectShiftSwapRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPost("cancel")]
    public Task<IActionResult> Cancel(CancelShiftSwapRequest request)
    {
        throw new NotImplementedException();
    }
}