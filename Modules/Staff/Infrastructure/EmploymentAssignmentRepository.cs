using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Modules.Staff.Application.Projections;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.BranchAssignments;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.PositionAssignments;
using ShiftManagement.Api.Infrastructure.Persistence;

namespace ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;

public class EmploymentAssignmentRepository(ShiftManagementDbContext context)
{

    // ------------------------
    // DTO PROJECTIONS (READ API)
    // ------------------------

    public Task<List<CollaboratorBranchResponse>> GetBranchesByEmployeeAsync(Guid employeeId)
        => context.Set<EmploymentAssignment>()
            .AsNoTracking()
            .Where(x => x.EmployeeId == employeeId && x.Type == AssignmentType.Branch)
            .ToBranchResponse()
            .ToListAsync();

    public Task<List<CollaboratorPositionResponse>> GetPositionsByEmployeeAsync(Guid employeeId)
        => context.Set<EmploymentAssignment>()
            .AsNoTracking()
            .Where(x => x.EmployeeId == employeeId && x.Type == AssignmentType.Position)
            .ToPositionResponse()
            .ToListAsync();

    // ------------------------
    // DOMAIN READ (FOR BUSINESS LOGIC)
    // ------------------------

    public Task<List<EmploymentAssignment>> GetByEmployeeAsync(Guid employeeId)
        => context.Set<EmploymentAssignment>()
            .Where(x => x.EmployeeId == employeeId)
            .ToListAsync();

    public Task<List<EmploymentAssignment>> GetByEmployeeAndTypeAsync(
        Guid employeeId,
        AssignmentType type
    )
        => context.Set<EmploymentAssignment>()
            .Where(x => x.EmployeeId == employeeId && x.Type == type)
            .ToListAsync();

    // ------------------------
    // COMMANDS
    // ------------------------

    public async Task AddAsync(EmploymentAssignment assignment)
        => await context.Set<EmploymentAssignment>().AddAsync(assignment);

    public void Update(EmploymentAssignment assignment)
        => context.Set<EmploymentAssignment>().Update(assignment);

    public void UpdateRange(IEnumerable<EmploymentAssignment> assignments)
        => context.Set<EmploymentAssignment>().UpdateRange(assignments);

    public void Remove(EmploymentAssignment assignment)
        => context.Set<EmploymentAssignment>().Remove(assignment);

    public Task<EmploymentAssignment?> GetByIdAsync(Guid id)
        => context.Set<EmploymentAssignment>()
            .FirstOrDefaultAsync(x => x.Id == id);
}