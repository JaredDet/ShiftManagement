using ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;
using ShiftManagement.Api.Modules.Claims.Infrastructure;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Infrastructure.Storage;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.BuildingBlocks.Storage;

namespace ShiftManagement.Api.Modules.Claims.Application.Submissions;

public sealed class AttachEvidenceToClaimUseCase(
    ClaimRepository claimRepository,
    ShiftManagementDbContext context,
    IFileStorage storage
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