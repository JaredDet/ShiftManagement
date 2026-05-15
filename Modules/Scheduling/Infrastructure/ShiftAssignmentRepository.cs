using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Scheduling.Domain;

namespace ShiftManagement.Api.Modules.Scheduling.Infrastructure;

public sealed class ShiftAssignmentRepository(ShiftManagementDbContext context)
{
    private readonly ShiftManagementDbContext _context = context;

    public async Task AddAsync(ShiftAssignment assignment)
    {
        await _context.Set<ShiftAssignment>().AddAsync(assignment);
    }

    public async Task<ShiftAssignment?> GetByIdAsync(Guid id)
    {
        return await _context.Set<ShiftAssignment>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ShiftAssignment?> GetActiveAssignmentAsync(Guid shiftId, Guid collaboratorId)
    {
        return await _context.Set<ShiftAssignment>()
            .FirstOrDefaultAsync(x =>
                x.ShiftId == shiftId &&
                x.CollaboratorId == collaboratorId &&
                x.Status == ShiftAssignmentStatus.Assigned);
    }

    public async Task<ShiftAssignment?> GetActiveByShiftIdAsync(Guid shiftId)
    {
        return await _context.Set<ShiftAssignment>()
            .FirstOrDefaultAsync(x =>
                x.ShiftId == shiftId &&
                x.Status == ShiftAssignmentStatus.Assigned
            );
    }

    public async Task<List<ShiftAssignment>> GetActiveAssignmentsByShiftsAsync(
    Guid sourceShiftId,
    Guid targetShiftId)
    {
        return await _context.Set<ShiftAssignment>()
            .Where(x =>
                (x.ShiftId == sourceShiftId || x.ShiftId == targetShiftId) &&
                x.Status == ShiftAssignmentStatus.Assigned)
            .ToListAsync();
    }

    public async Task<List<ShiftAssignment>> GetByShiftIdAsync(Guid shiftId)
    {
        return await _context.Set<ShiftAssignment>()
            .Where(x => x.ShiftId == shiftId)
            .ToListAsync();
    }

    public async Task<bool> ExistsActiveAssignmentAsync(Guid shiftId, Guid collaboratorId)
    {
        return await _context.Set<ShiftAssignment>()
            .AnyAsync(x =>
                x.ShiftId == shiftId &&
                x.CollaboratorId == collaboratorId &&
                x.Status == ShiftAssignmentStatus.Assigned
            );
    }
}