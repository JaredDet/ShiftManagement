using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;
using ShiftManagement.Api.Modules.Claims.Domain;
using ShiftManagement.Api.Modules.Claims.Infrastructure;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Claims.Application.Submissions;

public sealed class AttachEvidenceToClaimUseCase(
    ClaimRepository claimRepository,
    ShiftManagementDbContext context,
    LocalFileStorage storage
)
{
    public async Task<Result<ClaimResponse>> ExecuteAsync(
        Guid claimId,
        AttachEvidenceToClaimRequest request
    )
    {
        var claim = await claimRepository.GetByIdAsync(claimId);

        if (claim is null)
        {
            return Result<ClaimResponse>.Failure(
                ClaimErrors.ClaimNotFound
            );
        }

        var fileUrl = await storage.SaveAsync(
            StorageCategory.ClaimsEvidences,
            request.FileName,
            request.Base64Content,
            request.MimeType
        );

        claim.AddEvidence(
            request.FileName,
            fileUrl,
            request.MimeType
        );

        await context.SaveChangesAsync();

        return Result<ClaimResponse>.Success(
            ClaimMapper.ToResponse(claim)
        );
    }
}