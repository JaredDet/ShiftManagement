using Microsoft.AspNetCore.Mvc;

using ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;

namespace ShiftManagement.Api.Modules.Claims.Api.Controllers;

[ApiController]
[Route("api/claims/{claimId:guid}")]
public sealed class ClaimCommentController(
    AddCommentToClaimUseCase addCommentToClaimUseCase,
    AttachEvidenceToClaimUseCase attachEvidenceToClaimUseCase
) : ControllerBase
{
    [HttpPost("comments")]
    public async Task<IActionResult> AddComment(
        Guid claimId,
        [FromBody] AddCommentToClaimRequest request)
    {
        var result = await addCommentToClaimUseCase.ExecuteAsync(
            claimId,
            request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("evidences")]
    public async Task<IActionResult> AttachEvidence(
        Guid claimId,
        [FromBody] AttachEvidenceToClaimRequest request)
    {
        var result = await attachEvidenceToClaimUseCase.ExecuteAsync(
            claimId,
            request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}