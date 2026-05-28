using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure.Persistence;
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
}