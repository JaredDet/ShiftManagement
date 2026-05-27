using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Queries;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Reviews;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;
using ShiftManagement.Api.Modules.Claims.Application.Retrieval;
using ShiftManagement.Api.Modules.Claims.Application.Submissions;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Claims.Api.Controllers;

[ApiController]
[Route("api/claims")]
public class ClaimController(
    CreateClaimUseCase create,
    UpdateClaimUseCase update,
    CancelClaimUseCase cancel,

    AssignClaimUseCase assign,
    ChangeClaimStatusUseCase changeStatus,

    ResolveClaimUseCase resolve,
    RejectClaimUseCase reject,

    GetClaimUseCase get,
    ListClaimsUseCase list
) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateClaimRequest request)
    {
        return (await create.ExecuteAsync(request))
            .Match(value =>
                CreatedAtAction(
                    nameof(GetById),
                    new { id = value.Id },
                    value
                )
            );
    }

    [HttpPut("{claimId:guid}")]
    public async Task<IActionResult> Update(
        Guid claimId,
        [FromBody] UpdateClaimRequest request)
    {
        return (await update.ExecuteAsync(
            claimId,
            request)).Match(Ok);
    }

    [HttpDelete("{claimId:guid}")]
    public async Task<IActionResult> Cancel(Guid claimId)
    {
        var result = await cancel.ExecuteAsync(claimId);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpPost("{claimId:guid}/assign")]
    public async Task<IActionResult> Assign(
        Guid claimId,
        [FromBody] AssignClaimRequest request)
    {
        var result = await assign.ExecuteAsync(
            claimId,
            request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpPost("{claimId:guid}/status")]
    public async Task<IActionResult> ChangeStatus(
        Guid claimId,
        [FromBody] ChangeClaimStatusRequest request)
    {
        var result = await changeStatus.ExecuteAsync(
            claimId,
            request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpPost("{claimId:guid}/resolve")]
    public async Task<IActionResult> Resolve(
        Guid claimId,
        [FromBody] ResolveClaimRequest request)
    {
        var result = await resolve.ExecuteAsync(
            claimId,
            request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpPost("{claimId:guid}/reject")]
    public async Task<IActionResult> Reject(
        Guid claimId,
        [FromBody] RejectClaimRequest request)
    {
        var result = await reject.ExecuteAsync(
            claimId,
            request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpGet("{claimId:guid}")]
    public async Task<IActionResult> GetById(Guid claimId)
    {
        var result = await get.ExecuteAsync(claimId);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return (await get.ExecuteAsync(claimId))
            .Match(Ok);
    }

    [HttpGet]
    public async Task<IActionResult> List(
        [FromQuery] ListClaimsRequest request)
    {
        return (await list.ExecuteAsync(request))
            .Match(Ok);
    }
}