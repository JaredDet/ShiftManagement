using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Scheduling.Domain;

namespace ShiftManagement.Api.Modules.Scheduling.Infrastructure;

public sealed class ShiftRepository(ShiftManagementDbContext context)
{
    public async Task AddAsync(Shift shift)
    {
        await context.Shifts.AddAsync(shift);
    }

    public async Task<Shift?> GetByIdAsync(Guid id)
    {
        return await context.Shifts
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Shift>> GetByBranchIdAsync(Guid branchId)
    {
        return await context.Shifts
            .Where(x => x.BranchId == branchId)
            .ToListAsync();
    }
}