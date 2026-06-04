using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Infrastructure.Auth;
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

    AssignClaimUseCase assign,
    CancelClaimUseCase cancel,
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
    [Authorize(Policy = AuthorizationPolicies.ClaimsWriteAccess)]
    public async Task<IActionResult> Create(
        [FromBody] CreateClaimRequest request)
    {
        return (await create.ExecuteAsync(request))
            .Match(value =>
                CreatedAtAction(
                    nameof(GetById),
                    new { claimId = value.Id },
                    value
                )
            );
    }

    [HttpPut("{claimId:guid}")]
    [Authorize(Policy = AuthorizationPolicies.ClaimsWriteAccess)]
    public async Task<IActionResult> Update(
        Guid claimId,
        [FromBody] UpdateClaimRequest request)
    {
        return (await update.ExecuteAsync(
            claimId,
            request)).Match(Ok);
    }

    [HttpPost("{claimId:guid}/cancel")]
    [Authorize(Policy = AuthorizationPolicies.ClaimsWriteAccess)]
    public async Task<IActionResult> Cancel(Guid claimId, [FromBody] CancelClaimRequest request)
    {
        return (await cancel.ExecuteAsync(claimId, request))
            .Match(Ok);
    }

    [HttpPost("{claimId:guid}/assign")]
    [Authorize(Policy = AuthorizationPolicies.ClaimsModerationAccess)]
    public async Task<IActionResult> Assign(
        Guid claimId,
        [FromBody] AssignClaimRequest request)
    {
        return (await assign.ExecuteAsync(
            claimId,
            request)).Match(Ok);
    }

    [HttpPost("{claimId:guid}/start-review")]
    [Authorize(Policy = AuthorizationPolicies.ClaimsModerationAccess)]
    public async Task<IActionResult> StartReview(Guid claimId)
    {
        return (await startReview.ExecuteAsync(claimId))
            .Match(Ok);
    }

    [HttpPost("{claimId:guid}/resolve")]
    [Authorize(Policy = AuthorizationPolicies.ClaimsModerationAccess)]
    public async Task<IActionResult> Resolve(Guid claimId, [FromBody] ResolveClaimRequest request)
    {
        return (await resolve.ExecuteAsync(claimId, request))
            .Match(Ok);
    }

    [HttpPost("{claimId:guid}/reject")]
    [Authorize(Policy = AuthorizationPolicies.ClaimsModerationAccess)]
    public async Task<IActionResult> Reject(Guid claimId, [FromBody] RejectClaimRequest request)
    {
        return (await reject.ExecuteAsync(claimId, request))
            .Match(Ok);
    }

    [HttpPost("{claimId:guid}/reopen")]
    [Authorize(Policy = AuthorizationPolicies.ClaimsModerationAccess)]
    public async Task<IActionResult> Reopen(Guid claimId, [FromBody] ReopenClaimRequest request)
    {
        return (await reopen.ExecuteAsync(claimId, request))
            .Match(Ok);
    }

    [HttpGet("{claimId:guid}")]
    [Authorize(Policy = AuthorizationPolicies.ClaimsReadAccess)]
    public async Task<IActionResult> GetById(Guid claimId)
    {
        return (await get.ExecuteAsync(claimId))
            .Match(Ok);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.ClaimsReadAccess)]
    public async Task<IActionResult> List(
        [FromQuery] ListClaimsRequest request)
    {
        return (await list.ExecuteAsync(request))
            .Match(Ok);
    }

    [HttpGet("me")]
    [Authorize(Policy = AuthorizationPolicies.ClaimsSelfAccess)]
    public async Task<IActionResult> ListMyClaims()
    {
        return (await listMyClaims.ExecuteAsync())
            .Match(Ok);
    }
}