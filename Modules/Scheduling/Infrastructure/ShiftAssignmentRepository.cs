using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Scheduling.Domain;

namespace ShiftManagement.Api.Modules.Scheduling.Infrastructure;

public sealed class ShiftAssignmentRepository(ShiftManagementDbContext context)
{

    public async Task AddAsync(ShiftAssignment assignment)
    {
        await context.Set<ShiftAssignment>().AddAsync(assignment);
    }

    public async Task<ShiftAssignment?> GetActiveAssignmentAsync(Guid shiftId, Guid collaboratorId)
    {
        return await context.Set<ShiftAssignment>()
            .FirstOrDefaultAsync(x =>
                x.ShiftId == shiftId &&
                x.CollaboratorId == collaboratorId &&
                x.Status == ShiftAssignmentStatus.Assigned);
    }

    public async Task<ShiftAssignment?> GetActiveByShiftIdAsync(Guid shiftId)
    {
        return await context.Set<ShiftAssignment>()
            .FirstOrDefaultAsync(x =>
                x.ShiftId == shiftId &&
                x.Status == ShiftAssignmentStatus.Assigned
            );
    }

    public async Task<bool> ExistsActiveAssignmentAsync(Guid shiftId, Guid collaboratorId)
    {
        return await context.Set<ShiftAssignment>()
            .AnyAsync(x =>
                x.ShiftId == shiftId &&
                x.CollaboratorId == collaboratorId &&
                x.Status == ShiftAssignmentStatus.Assigned
            );
    }
}