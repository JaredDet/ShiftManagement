using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Application.Swaps;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/shift-swaps")]
public sealed class ShiftSwapController(
    RequestShiftSwapUseCase requestUseCase,
    ApproveShiftSwapUseCase approve,
    RespondToShiftSwapUseCase respond,
    CancelShiftSwapUseCase cancel
) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = "StaffOnly")]
    public async Task<IActionResult> RequestSwap(
        [FromBody] RequestShiftSwapRequest request
    )
    {
        return (await requestUseCase.ExecuteAsync(request))
            .Match(Ok);
    }

    [HttpPost("{id:guid}/approve")]
    [Authorize(Policy = "ShiftManagementAccess")]
    public async Task<IActionResult> Approve(Guid id)
    {
        return (await approve.ExecuteAsync(id))
            .Match(Ok);
    }

    [HttpPost("{id:guid}/accept")]
    [Authorize(Policy = "StaffOnly")]
    public async Task<IActionResult> Accept(Guid id)
    {
        return (await respond.ExecuteAsync(
            id,
            ShiftSwapDecision.Accept
        )).Match(Ok);
    }

    [HttpPost("{id:guid}/reject")]
    [Authorize(Policy = "SwapRejectAccess")]
    public async Task<IActionResult> Reject(Guid id)
    {
        return (await respond.ExecuteAsync(
            id,
            ShiftSwapDecision.Reject
        )).Match(Ok);

    }

    [HttpPost("{id:guid}/cancel")]
    [Authorize(Policy = "StaffOnly")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        return (await cancel.ExecuteAsync(id))
            .Match(Ok);
    }
}