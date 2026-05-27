using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Claims.Domain;

namespace ShiftManagement.Api.Modules.Claims.Infrastructure;

public sealed class ClaimRepository(ShiftManagementDbContext context)
{
    public async Task AddAsync(Claim claim)
    {
        await context.Claims.AddAsync(claim);
    }

    public async Task<Claim?> GetByIdAsync(Guid id)
    {
        return await context.Claims
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Claim>> GetByCompanyIdAsync(Guid companyId)
    {
        return await context.Claims
            .Where(x => x.CompanyId == companyId)
            .ToListAsync();
    }

    public async Task<List<Claim>> GetByCollaboratorIdAsync(Guid collaboratorId)
    {
        return await context.Claims
            .Where(x => x.CollaboratorId == collaboratorId)
            .ToListAsync();
    }

    public async Task<List<Claim>> GetAssignedToUserAsync(Guid userId)
    {
        return await context.Claims
            .Where(x => x.AssignedToUserId == userId)
            .ToListAsync();
    }

    public async Task<List<Claim>> GetOpenClaimsAsync()
    {
        return await context.Claims
            .Where(x =>
                x.Status != ClaimStatus.Resolved &&
                x.Status != ClaimStatus.Rejected &&
                x.Status != ClaimStatus.Canceled)
            .ToListAsync();
    }
}