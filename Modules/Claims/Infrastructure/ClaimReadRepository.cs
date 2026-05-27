using Microsoft.EntityFrameworkCore;

using ShiftManagement.Api.Infrastructure;

using ShiftManagement.Api.Modules.Claims.Api.Contracts.Queries;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;

using ShiftManagement.Api.Modules.Claims.Application;

namespace ShiftManagement.Api.Modules.Claims.Infrastructure;

public sealed class ClaimReadRepositoryPostgres(
    ShiftManagementDbContext context
)
{
    public async Task<ClaimResponse?> GetByIdAsync(
        Guid claimId
    )
    {
        return await context
            .ToClaimResponse()
            .FirstOrDefaultAsync(x => x.Id == claimId);
    }

    public async Task<List<ClaimResponse>> ListAsync(
        ListClaimsRequest request
    )
    {
        return await context
            .ToClaimResponse()
            .ApplyFilters(request)
            .ToListAsync();
    }
}