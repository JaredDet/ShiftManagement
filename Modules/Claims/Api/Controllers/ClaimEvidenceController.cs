using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;
using ShiftManagement.Api.Modules.Claims.Application.Submissions;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Claims.Api.Controllers;

[ApiController]
[Route("api/claims/{claimId:guid}")]
public class ClaimEvidenceController(
    AttachEvidenceToClaimUseCase attachEvidence
) : ControllerBase
{

    [HttpPost("evidences")]
    public async Task<IActionResult> AttachEvidence(
        Guid claimId,
        [FromBody] AttachEvidenceToClaimRequest request)
    {
        return (await attachEvidence.ExecuteAsync(
            claimId,
            request)).Match(Ok);
    }
}