using ShiftManagement.Api.Infrastructure;

using ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;

using ShiftManagement.Api.Modules.Claims.Infrastructure;

using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Claims.Application.Reviews;

public sealed class ResolveClaimUseCase(
    ClaimRepository claimRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<ClaimResponse>> ExecuteAsync(
        Guid claimId
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

        await context.SaveChangesAsync();

        return Result<ClaimResponse>.Success(
            ClaimMapper.ToResponse(claim)
        );
    }
}