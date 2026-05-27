using ShiftManagement.Api.Infrastructure;

using ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Reviews;
using ShiftManagement.Api.Modules.Claims.Domain;
using ShiftManagement.Api.Modules.Claims.Infrastructure;

using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Claims.Application.Reviews;

public sealed class RejectClaimUseCase(
    ClaimRepository claimRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<ClaimResponse>> ExecuteAsync(
        Guid claimId,
        RejectClaimRequest request
    )
    {
        var claim = await claimRepository.GetByIdAsync(claimId);

        if (claim is null)
        {
            return Result<ClaimResponse>.Failure(
                ClaimErrors.ClaimNotFound
            );
        }

        claim.Reject();
        claim.AddComment(request.ReviewerId, request.Reason, ClaimCommentType.Rejection);

        await context.SaveChangesAsync();

        return Result<ClaimResponse>.Success(
            ClaimMapper.ToResponse(claim)
        );
    }
}