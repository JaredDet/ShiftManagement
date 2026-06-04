using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Organization.Domain;

namespace ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

public sealed class CompanyRepository(ShiftManagementDbContext context)
{

    public Task<Company?> GetByIdAsync(Guid id)
    {
        return context.Companies
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task<List<Company>> ListAsync()
    {
        return context.Companies
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return context.Companies
            .AnyAsync(c => c.Id == id);
    }
}