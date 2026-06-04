using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Infrastructure.Auth;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;
using ShiftManagement.Api.Modules.Claims.Application.Submissions;

namespace ShiftManagement.Api.Modules.Claims.Api.Controllers;

[ApiController]
[Route("api/claims/{claimId:guid}")]
public class ClaimEvidenceController(
    AttachEvidenceToClaimUseCase attachEvidence
) : ControllerBase
{

    [HttpPost("evidences")]
    [Authorize(Policy = AuthorizationPolicies.ClaimsEvidenceUploadAccess)]
    public async Task<IActionResult> AttachEvidence(
        Guid claimId,
        [FromBody] AttachEvidenceToClaimRequest request)
    {
        return (await attachEvidence.ExecuteAsync(
            claimId,
            request)).Match(Ok);
    }
}