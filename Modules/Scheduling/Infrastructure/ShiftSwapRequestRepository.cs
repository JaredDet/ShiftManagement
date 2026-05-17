using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Scheduling.Domain;

namespace ShiftManagement.Api.Modules.Scheduling.Infrastructure;

public sealed class ShiftSwapRequestRepository(ShiftManagementDbContext context)
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

    public async Task<List<ShiftSwapRequest>> GetByRequesterIdAsync(Guid requesterId)
    {
        return await context.Set<ShiftSwapRequest>()
            .Where(x => x.RequesterId == requesterId)
            .ToListAsync();
    }

    public async Task<List<ShiftSwapRequest>> GetByTargetCollaboratorIdAsync(Guid collaboratorId)
    {
        return await context.Set<ShiftSwapRequest>()
            .Where(x => x.TargetCollaboratorId == collaboratorId)
            .ToListAsync();
    }

    public async Task<List<ShiftSwapRequest>> GetPendingByCollaboratorIdAsync(Guid collaboratorId)
    {
        return await context.Set<ShiftSwapRequest>()
            .Where(x =>
                x.TargetCollaboratorId == collaboratorId &&
                x.Status == ShiftSwapStatus.Pending)
            .ToListAsync();
    }

    public async Task<bool> ExistsActiveSwapBetweenShiftsAsync(Guid sourceShiftId, Guid targetShiftId)
    {
        return await context.Set<ShiftSwapRequest>()
            .AnyAsync(x =>
                x.SourceShiftId == sourceShiftId &&
                x.TargetShiftId == targetShiftId &&
                x.Status != ShiftSwapStatus.Cancelled &&
                x.Status != ShiftSwapStatus.Rejected);
    }
}