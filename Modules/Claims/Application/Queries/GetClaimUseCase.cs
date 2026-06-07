using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;

using ShiftManagement.Api.Modules.Claims.Infrastructure;

namespace ShiftManagement.Api.Modules.Claims.Application.Queries;

public sealed class GetClaimUseCase(
    ClaimReadRepository claimReadRepository
)
{
    public async Task<Result<ClaimResponse>> ExecuteAsync(
        Guid claimId
    )
    {
        var claim = await claimReadRepository.GetByIdAsync(claimId);

        if (claim is null)
        {
            return Result<ClaimResponse>.Failure(
                ClaimErrors.ClaimNotFound
            );
        }

        return Result<ClaimResponse>.Success(claim);
    }
}