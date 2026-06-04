using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Projections;

namespace ShiftManagement.Api.Modules.Staff.Infrastructure;

public class PositionReadRepository(ShiftManagementDbContext context)
{

    public Task<PositionResponse?> GetByIdProjectedAsync(Guid id)
    {
        return context.Set<Position>()
            .AsNoTracking()
            .Where(p => p.Id == id)
            .ToPositionResponse()
            .FirstOrDefaultAsync();
    }

    public Task<List<PositionResponse>> ListProjectedAsync()
    {
        return context.Set<Position>()
            .AsNoTracking()
            .ToPositionResponse()
            .ToListAsync();
    }
}