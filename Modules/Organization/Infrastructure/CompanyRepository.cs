using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Organization.Domain;

namespace ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

public sealed class CompanyRepository
{
    private readonly ShiftManagementDbContext _context;

    public CompanyRepository(ShiftManagementDbContext context)
    {
        _context = context;
    }

    public Task<Company?> GetByIdAsync(Guid id)
    {
        return _context.Companies
            .FirstOrDefaultAsync(company => company.Id == id);
    }

    public Task<List<Company>> ListAsync()
    {
        return _context.Set<Company>()
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<List<Company>> GetAllAsync()
    {
        return _context.Companies
            .ToListAsync();
    }

    public async Task AddAsync(Company company)
    {
        await _context.Companies.AddAsync(company);
    }

    public Task UpdateAsync(Company company)
    {
        _context.Companies.Update(company);

        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _context.Companies
            .AnyAsync(company => company.Id == id);
    }
}