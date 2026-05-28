using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Reviews;

using ShiftManagement.Api.Modules.Claims.Infrastructure;

using ShiftManagement.Api.Modules.Identity.Application;
using ShiftManagement.Api.Modules.Identity.Infrastructure;

namespace ShiftManagement.Api.Modules.Claims.Application.Reviews;

public sealed class AssignClaimUseCase(
    ClaimRepository claimRepository,
    UserRepository userRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<ClaimResponse>> ExecuteAsync(
        Guid claimId,
        AssignClaimRequest request
    )
    {
        var claimTask =
            claimRepository.GetByIdAsync(claimId);

        var userExistsTask =
            userRepository.ExistsAsync(request.AssignedToUserId);

        await Task.WhenAll(
            claimTask,
            userExistsTask
        );

        var claim = claimTask.Result;

        if (claim is null)
        {
            return Result<ClaimResponse>.Failure(
                ClaimErrors.ClaimNotFound
            );
        }

        if (!userExistsTask.Result)
        {
            return Result<ClaimResponse>.Failure(
                IdentityErrors.UserNotFound
            );
        }

        claim.Assign(request.AssignedToUserId);

        await context.SaveChangesAsync();

        return Result<ClaimResponse>.Success(
            ClaimMapper.ToResponse(claim)
        );
    }
}