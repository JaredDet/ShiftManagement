using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Application.Swaps;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Controllers;

[ApiController]
[Route("api/shifts/swaps")]
public class ShiftSwapController(
    RequestShiftSwapUseCase requestUseCase,
    ApproveShiftSwapUseCase approveUseCase,
    RespondToShiftSwapUseCase respondUseCase,
    CancelShiftSwapUseCase cancelUseCase
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RequestSwap(RequestShiftSwapRequest request)
    {
        var result = await requestUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("approve")]
    public async Task<IActionResult> Approve(ApproveShiftSwapRequest request)
    {
        var result = await approveUseCase.ExecuteAsync(request.SwapRequestId);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("respond")]
    public async Task<IActionResult> Respond(RespondShiftSwapRequest request)
    {
        var result = await respondUseCase.ExecuteAsync(
            request.SwapRequestId,
            request.Decision
        );

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("cancel")]
    public async Task<IActionResult> Cancel(CancelShiftSwapRequest request)
    {
        var result = await cancelUseCase.ExecuteAsync(request.SwapRequestId);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}