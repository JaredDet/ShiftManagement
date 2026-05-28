using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Queries;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;

using ShiftManagement.Api.Modules.Claims.Infrastructure;

namespace ShiftManagement.Api.Modules.Claims.Application.Retrievals;

public sealed class ListClaimsUseCase(
    ClaimReadRepositoryPostgres claimReadRepository
)
{
    public async Task<Result<List<ClaimResponse>>> ExecuteAsync(
        ListClaimsRequest request
    )
    {
        var claims = await claimReadRepository.ListAsync(request);

        return Result<List<ClaimResponse>>.Success(claims);
    }
}