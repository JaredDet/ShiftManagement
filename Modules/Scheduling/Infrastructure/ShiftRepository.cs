using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure.Persistence;
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
}