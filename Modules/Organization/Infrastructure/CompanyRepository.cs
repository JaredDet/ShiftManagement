using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Organization.Domain;

namespace ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

public sealed class CompanyRepository(ShiftManagementDbContext context)
{
    private readonly ShiftManagementDbContext _context = context;

    public Task<Company?> GetByIdAsync(Guid id)
    {
        return _context.Companies
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task<List<Company>> ListAsync()
    {
        return _context.Companies
            .AsNoTracking()
            .ToListAsync();
    }

    public Task AddAsync(Company company)
    {
        return _context.Companies.AddAsync(company).AsTask();
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _context.Companies
            .AnyAsync(c => c.Id == id);
    }

    public Task<bool> ExistsByNameAsync(string name)
    {
        return _context.Companies
            .AnyAsync(c => c.Name.ToLower() == name.ToLower());
    }
}