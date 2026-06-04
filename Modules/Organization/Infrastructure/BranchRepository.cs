using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Organization.Domain;

namespace ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

public sealed class BranchRepository(ShiftManagementDbContext context)
{

    public Task<Branch?> GetByIdAsync(Guid id)
    {
        return context.Branches
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public Task<List<Branch>> GetByCompanyIdAsync(Guid companyId)
    {
        return context.Branches
            .AsNoTracking()
            .Where(b => b.CompanyId == companyId)
            .ToListAsync();
    }

    public Task AddAsync(Branch branch)
    {
        return context.Branches.AddAsync(branch).AsTask();
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return context.Branches
            .AnyAsync(b => b.Id == id);
    }

    public Task<bool> ExistsByNameAndCompanyAsync(string name, Guid companyId)
    {
        return context.Branches
            .AnyAsync(b =>
                b.CompanyId == companyId &&
                b.Name.ToLower() == name.ToLower());
    }
}