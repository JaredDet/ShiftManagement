using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Modules.Staff.Application.Projections;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;
using ShiftManagement.Api.Infrastructure.Persistence;

namespace ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;

public class PositionRepository(ShiftManagementDbContext context)
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

    public Task<Position?> GetByIdAsync(Guid id)
    {
        return context.Set<Position>()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Position position)
    {
        await context.Set<Position>().AddAsync(position);
    }

    public void Update(Position position)
    {
        context.Set<Position>().Update(position);
    }

    public void Remove(Position position)
    {
        context.Set<Position>().Remove(position);
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return context.Set<Position>()
            .AnyAsync(p => p.Id == id);
    }

    public Task<bool> ExistsByNameAsync(
    Guid companyId,
    string name
    )
    {
        return context.Set<Position>()
            .AnyAsync(position =>
                position.CompanyId == companyId &&
                position.Name.ToLower() == name
            );
    }
}