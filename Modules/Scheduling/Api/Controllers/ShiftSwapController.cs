using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Application.Swaps;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Controllers;

[Authorize]
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
    [Authorize(Policy = "StaffOnly")]
    public async Task<IActionResult> RequestSwap(
        [FromBody] RequestShiftSwapRequest request
    )
    {
        var result = await requestUseCase.ExecuteAsync(request);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "scheduling.shift.not_found" => NotFound(result.Error),

            "scheduling.assignment.not_found" => NotFound(result.Error),

            "scheduling.shift.invalid_state" => BadRequest(result.Error),

            "scheduling.shift_swap.invalid_transition" => BadRequest(result.Error),

            _ => BadRequest(result.Error)
        };
    }

    [HttpPost("{id:guid}/approve")]
    [Authorize(Policy = "ShiftManagementAccess")]
    public async Task<IActionResult> Approve(Guid id)
    {
        var result = await approveUseCase.ExecuteAsync(id);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "scheduling.swap_request.not_found" => NotFound(result.Error),

            "scheduling.assignment.not_found" => NotFound(result.Error),

            "scheduling.shift_swap.invalid_transition" => BadRequest(result.Error),

            _ => BadRequest(result.Error)
        };
    }

    [HttpPost("{id:guid}/accept")]
    [Authorize(Policy = "StaffOnly")]
    public async Task<IActionResult> Accept(Guid id)
    {
        var result = await respondUseCase.ExecuteAsync(
            id,
            ShiftSwapDecision.Accept
        );

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "scheduling.swap_request.not_found" => NotFound(result.Error),

            "scheduling.invalid_swap_decision" => BadRequest(result.Error),

            "scheduling.shift_swap.invalid_transition" => BadRequest(result.Error),

            _ => BadRequest(result.Error)
        };
    }

    [HttpPost("{id:guid}/reject")]
    [Authorize(Policy = "SwapRejectAccess")]
    public async Task<IActionResult> Reject(Guid id)
    {
        var result = await respondUseCase.ExecuteAsync(
            id,
            ShiftSwapDecision.Reject
        );

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "scheduling.swap_request.not_found" => NotFound(result.Error),

            "scheduling.invalid_swap_decision" => BadRequest(result.Error),

            "scheduling.shift_swap.invalid_transition" => BadRequest(result.Error),

            _ => BadRequest(result.Error)
        };
    }

    [HttpPost("{id:guid}/cancel")]
    [Authorize(Policy = "StaffOnly")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var result = await cancelUseCase.ExecuteAsync(id);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "scheduling.swap_request.not_found" => NotFound(result.Error),

            "scheduling.shift_swap.invalid_transition" => BadRequest(result.Error),

            _ => BadRequest(result.Error)
        };
    }
}