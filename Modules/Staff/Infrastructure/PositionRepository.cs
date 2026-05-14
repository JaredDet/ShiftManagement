using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Modules.Staff.Application.Projections;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;
using ShiftManagement.Api.Infrastructure;

namespace ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;

public class PositionRepository
{
    private readonly ShiftManagementDbContext _context;

    public PositionRepository(ShiftManagementDbContext context)
    {
        _context = context;
    }

    public Task<PositionResponse?> GetByIdProjectedAsync(Guid id)
    {
        return _context.Set<Position>()
            .AsNoTracking()
            .Where(p => p.Id == id)
            .ToPositionResponse()
            .FirstOrDefaultAsync();
    }

    public Task<List<PositionResponse>> ListProjectedAsync()
    {
        return _context.Set<Position>()
            .AsNoTracking()
            .ToPositionResponse()
            .ToListAsync();
    }

    public Task<Position?> GetByIdAsync(Guid id)
    {
        return _context.Set<Position>()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Position position)
    {
        await _context.Set<Position>().AddAsync(position);
    }

    public void Update(Position position)
    {
        _context.Set<Position>().Update(position);
    }

    public void Remove(Position position)
    {
        _context.Set<Position>().Remove(position);
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _context.Set<Position>()
            .AnyAsync(p => p.Id == id);
    }
}