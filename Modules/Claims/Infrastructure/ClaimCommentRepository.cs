using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Claims.Domain;

namespace ShiftManagement.Api.Modules.Claims.Infrastructure;

public sealed class ClaimCommentRepository(ShiftManagementDbContext context)
{
    public async Task AddAsync(ClaimComment comment)
    {
        await context.ClaimComments.AddAsync(comment);
    }

    public async Task<ClaimComment?> GetByIdAsync(Guid id)
    {
        return await context.ClaimComments
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<ClaimComment>> GetByClaimIdAsync(Guid claimId)
    {
        return await context.ClaimComments
            .Where(x => x.ClaimId == claimId)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task DeleteAsync(ClaimComment comment)
    {
        context.ClaimComments.Remove(comment);
    }
}