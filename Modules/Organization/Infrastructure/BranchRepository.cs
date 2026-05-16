using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Organization.Domain;

namespace ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

public sealed class BranchRepository(ShiftManagementDbContext context)
{
    private readonly ShiftManagementDbContext _context = context;

    public Task<Branch?> GetByIdAsync(Guid id)
    {
        return _context.Branches
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public Task<List<Branch>> GetByCompanyIdAsync(Guid companyId)
    {
        return _context.Branches
            .AsNoTracking()
            .Where(b => b.CompanyId == companyId)
            .ToListAsync();
    }

    public Task<List<Branch>> ListAsync()
    {
        return _context.Branches
            .AsNoTracking()
            .ToListAsync();
    }

    public Task AddAsync(Branch branch)
    {
        return _context.Branches.AddAsync(branch).AsTask();
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _context.Branches
            .AnyAsync(b => b.Id == id);
    }

    public Task<bool> ExistsByNameAndCompanyAsync(string name, Guid companyId)
    {
        return _context.Branches
            .AnyAsync(b =>
                b.CompanyId == companyId &&
                b.Name.ToLower() == name.ToLower());
    }
}