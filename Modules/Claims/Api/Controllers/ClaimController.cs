using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Queries;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Reviews;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;
using ShiftManagement.Api.Modules.Claims.Application.Retrievals;
using ShiftManagement.Api.Modules.Claims.Application.Reviews;
using ShiftManagement.Api.Modules.Claims.Application.Submissions;

namespace ShiftManagement.Api.Modules.Claims.Api.Controllers;

[ApiController]
[Route("api/claims")]
public class ClaimController(
    CreateClaimUseCase create,
    UpdateClaimUseCase update,
    CancelClaimUseCase cancel,

    AssignClaimUseCase assign,
    StartClaimReviewUseCase startReview,
    ResolveClaimUseCase resolve,
    RejectClaimUseCase reject,
    ReopenClaimUseCase reopen,

    GetClaimUseCase get,
    ListClaimsUseCase list,
    ListMyClaimsUseCase listMyClaims
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

    [HttpPost("{claimId:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid claimId, [FromBody] CancelClaimRequest request)
    {
        return (await cancel.ExecuteAsync(claimId, request))
            .Match(Ok);
    }

    [HttpPost("{claimId:guid}/assign")]
    public async Task<IActionResult> Assign(
        Guid claimId,
        [FromBody] AssignClaimRequest request)
    {
        return (await assign.ExecuteAsync(
            claimId,
            request)).Match(Ok);
    }

    [HttpPost("{claimId:guid}/start-review")]
    public async Task<IActionResult> StartReview(Guid claimId)
    {
        return (await startReview.ExecuteAsync(claimId))
            .Match(Ok);
    }

    [HttpPost("{claimId:guid}/resolve")]
    public async Task<IActionResult> Resolve(Guid claimId, [FromBody] ResolveClaimRequest request)
    {
        return (await resolve.ExecuteAsync(claimId, request))
            .Match(Ok);
    }

    [HttpPost("{claimId:guid}/reject")]
    public async Task<IActionResult> Reject(Guid claimId, [FromBody] RejectClaimRequest request)
    {
        return (await reject.ExecuteAsync(claimId, request))
            .Match(Ok);
    }

    [HttpPost("{claimId:guid}/reopen")]
    public async Task<IActionResult> Reopen(Guid claimId, [FromBody] ReopenClaimRequest request)
    {
        return (await reopen.ExecuteAsync(claimId, request))
            .Match(Ok);
    }

    [HttpGet("{claimId:guid}")]
    public async Task<IActionResult> GetById(Guid claimId)
    {
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

    [HttpGet("me")]
    public async Task<IActionResult> GetMyClaims()
    {
        return (await listMyClaims.ExecuteAsync())
            .Match(Ok);
    }
}