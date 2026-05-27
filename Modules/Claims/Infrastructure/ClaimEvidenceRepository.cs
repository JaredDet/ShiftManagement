using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Claims.Domain;

namespace ShiftManagement.Api.Modules.Claims.Infrastructure;

public sealed class ClaimEvidenceRepository(ShiftManagementDbContext context)
{
    public async Task AddAsync(ClaimEvidence evidence)
    {
        await context.ClaimEvidences.AddAsync(evidence);
    }

    public async Task<ClaimEvidence?> GetByIdAsync(Guid id)
    {
        return await context.ClaimEvidences
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<ClaimEvidence>> GetByClaimIdAsync(Guid claimId)
    {
        return await context.ClaimEvidences
            .Where(x => x.ClaimId == claimId)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task DeleteAsync(ClaimEvidence evidence)
    {
        context.ClaimEvidences.Remove(evidence);
    }
}