using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Infrastructure.Persistence;

namespace ShiftManagement.Api.Modules.Staff.Infrastructure;

public class PositionRepository(ShiftManagementDbContext context)
{
    public Task<Position?> GetByIdAsync(Guid id)
    {
        return context.Set<Position>()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Position position)
    {
        await context.Set<Position>().AddAsync(position);
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