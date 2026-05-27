using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;
using ShiftManagement.Api.Modules.Claims.Application;

namespace ShiftManagement.Api.Modules.Claims.Infrastructure;

public sealed class ClaimReadRepositoryPostgres(ShiftManagementDbContext context)
{
    public IQueryable<ClaimResponse> Query()
    {
        return context.ToClaimResponse();
    }

    public async Task<ClaimResponse?> GetByIdAsync(Guid id)
    {
        return await context.ToClaimResponse()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<ClaimResponse>> GetByCompanyIdAsync(Guid companyId)
    {
        return await context.ToClaimResponse()
            .Where(x => x.CompanyId == companyId)
            .ToListAsync();
    }
}