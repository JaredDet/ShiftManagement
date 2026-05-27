using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Queries;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Reviews;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;
using ShiftManagement.Api.Modules.Claims.Application.Submissions;

namespace ShiftManagement.Api.Modules.Claims.Api.Controllers;

[ApiController]
[Route("api/claims")]
public sealed class ClaimController(
    CreateClaimUseCase createClaimUseCase,
    UpdateClaimUseCase updateClaimUseCase,
    CancelClaimUseCase cancelClaimUseCase,

    AssignClaimUseCase assignClaimUseCase,
    ChangeClaimStatusUseCase changeClaimStatusUseCase,

    ResolveClaimUseCase resolveClaimUseCase,
    RejectClaimUseCase rejectClaimUseCase,

    GetClaimUseCase getClaimUseCase,
    ListClaimsUseCase listClaimsUseCase
) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateClaimRequest request)
    {
        var result = await createClaimUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
        {
            return result.Error.Code switch
            {
                "organization.company.not_found" => NotFound(result.Error),
                "staff.employee.not_found" => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }

        return CreatedAtAction(
            nameof(GetById),
            new { claimId = result.Value.Id },
            result.Value);
    }

    [HttpPut("{claimId:guid}")]
    public async Task<IActionResult> Update(
        Guid claimId,
        [FromBody] UpdateClaimRequest request)
    {
        var result = await updateClaimUseCase.ExecuteAsync(
            claimId,
            request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete("{claimId:guid}")]
    public async Task<IActionResult> Cancel(Guid claimId)
    {
        var result = await cancelClaimUseCase.ExecuteAsync(claimId);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpPost("{claimId:guid}/assign")]
    public async Task<IActionResult> Assign(
        Guid claimId,
        [FromBody] AssignClaimRequest request)
    {
        var result = await assignClaimUseCase.ExecuteAsync(
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
        var result = await changeClaimStatusUseCase.ExecuteAsync(
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
        var result = await resolveClaimUseCase.ExecuteAsync(
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
        var result = await rejectClaimUseCase.ExecuteAsync(
            claimId,
            request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpGet("{claimId:guid}")]
    public async Task<IActionResult> GetById(Guid claimId)
    {
        var result = await getClaimUseCase.ExecuteAsync(claimId);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> List(
        [FromQuery] ListClaimsRequest request)
    {
        var result = await listClaimsUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}