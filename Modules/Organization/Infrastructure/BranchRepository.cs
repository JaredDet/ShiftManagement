using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Organization.Domain;

namespace ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

public sealed class BranchRepository(
    ShiftManagementDbContext dbContext
)
{
    public async Task<Branch?> GetByIdAsync(Guid id)
    {
        return await dbContext.Branches
            .FirstOrDefaultAsync(branch => branch.Id == id);
    }

    public async Task<List<Branch>> GetByCompanyIdAsync(Guid companyId)
    {
        return await dbContext.Branches
            .Where(branch => branch.CompanyId == companyId)
            .ToListAsync();
    }

    public async Task<List<Branch>> GetAllAsync()
    {
        return await dbContext.Branches
            .ToListAsync();
    }

    public async Task AddAsync(Branch branch)
    {
        await dbContext.Branches.AddAsync(branch);
    }

    public Task UpdateAsync(Branch branch)
    {
        dbContext.Branches.Update(branch);

        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}