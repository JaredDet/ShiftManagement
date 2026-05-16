using Microsoft.AspNetCore.Mvc;

using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Application.Swaps;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Controllers;

[ApiController]
[Route("api/shift-swaps")]
public sealed class ShiftSwapController(
    RequestShiftSwapUseCase requestUseCase,
    ApproveShiftSwapUseCase approveUseCase,
    RespondToShiftSwapUseCase respondUseCase,
    CancelShiftSwapUseCase cancelUseCase
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RequestSwap(
        RequestShiftSwapRequest request)
    {
        var result = await requestUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("{id}/approve")]
    public async Task<IActionResult> Approve(Guid id)
    {
        var result = await approveUseCase.ExecuteAsync(id);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("{id}/accept")]
    public async Task<IActionResult> Accept(Guid id)
    {
        var result = await respondUseCase.ExecuteAsync(
            id,
            ShiftSwapDecision.Accept
        );

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("{id}/reject")]
    public async Task<IActionResult> Reject(Guid id)
    {
        var result = await respondUseCase.ExecuteAsync(
            id,
            ShiftSwapDecision.Reject
        );

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var result = await cancelUseCase.ExecuteAsync(id);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}