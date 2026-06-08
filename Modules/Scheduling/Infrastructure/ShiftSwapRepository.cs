using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Scheduling.Domain;

namespace ShiftManagement.Api.Modules.Scheduling.Infrastructure;

public sealed class ShiftSwapRepository(ShiftManagementDbContext context)
{

    public async Task AddAsync(ShiftSwapRequest swapRequest)
    {
        await context.Set<ShiftSwapRequest>().AddAsync(swapRequest);
    }

    public async Task<ShiftSwapRequest?> GetByIdAsync(Guid id)
    {
        return await context.Set<ShiftSwapRequest>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}