using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Organization.Domain;

namespace ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

public sealed class CompanyRepository(
    ShiftManagementDbContext dbContext
)
{
    public async Task<Company?> GetByIdAsync(Guid id)
    {
        return await dbContext.Companies
            .FirstOrDefaultAsync(company => company.Id == id);
    }

    public async Task<List<Company>> GetAllAsync()
    {
        return await dbContext.Companies
            .ToListAsync();
    }

    public async Task AddAsync(Company company)
    {
        await dbContext.Companies.AddAsync(company);
    }

    public Task UpdateAsync(Company company)
    {
        dbContext.Companies.Update(company);

        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}