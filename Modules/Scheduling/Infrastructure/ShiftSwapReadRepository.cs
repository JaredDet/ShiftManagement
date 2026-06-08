using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Application.Swaps;

namespace ShiftManagement.Api.Modules.Scheduling.Infrastructure;

public sealed class ShiftSwapReadRepository(ShiftManagementDbContext context)
{
    public Task<ShiftSwapRequestResponse?> GetByIdAsync(Guid id)
    {
        return context.ToShiftSwapResponse()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}