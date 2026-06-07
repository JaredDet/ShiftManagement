using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Reviews;
using ShiftManagement.Api.Modules.Claims.Domain;
using ShiftManagement.Api.Modules.Claims.Infrastructure;

namespace ShiftManagement.Api.Modules.Claims.Application.Reviews;

public sealed class ResolveClaimUseCase(
    ClaimRepository claimRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<ClaimResponse>> ExecuteAsync(
        Guid claimId,
        ResolveClaimRequest request
    )
    {
        var claim = await claimRepository.GetByIdAsync(claimId);

        if (claim is null)
        {
            return Result<ClaimResponse>.Failure(
                ClaimErrors.ClaimNotFound
            );
        }

        claim.Resolve();
        claim.AddComment(request.ReviewerId, request.ResolutionComment, ClaimCommentType.Resolution);

        await context.SaveChangesAsync();

        return Result<ClaimResponse>.Success(
            ClaimMapper.ToResponse(claim)
        );
    }
}