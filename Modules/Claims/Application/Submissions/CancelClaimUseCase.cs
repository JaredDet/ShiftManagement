using ShiftManagement.Api.Infrastructure;

using ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;

using ShiftManagement.Api.Modules.Claims.Infrastructure;

using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Claims.Application.Submissions;

public sealed class CancelClaimUseCase(
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

        claim.Cancel();

        await context.SaveChangesAsync();

        return Result<ClaimResponse>.Success(
            ClaimMapper.ToResponse(claim)
        );
    }
}