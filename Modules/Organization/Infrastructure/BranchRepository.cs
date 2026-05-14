using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Organization.Domain;

namespace ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

public sealed class BranchRepository
{
    private readonly ShiftManagementDbContext _context;

    public BranchRepository(ShiftManagementDbContext context)
    {
        _context = context;
    }

    public Task<Branch?> GetByIdAsync(Guid id)
    {
        return _context.Branches
            .FirstOrDefaultAsync(branch => branch.Id == id);
    }

    public Task<List<Branch>> GetByCompanyIdAsync(Guid companyId)
    {
        return _context.Branches
            .AsNoTracking()
            .Where(branch => branch.CompanyId == companyId)
            .ToListAsync();
    }

    public Task<List<Branch>> ListAsync()
    {
        return _context.Branches
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Branch branch)
    {
        await _context.Branches.AddAsync(branch);
    }

    public Task UpdateAsync(Branch branch)
    {
        _context.Branches.Update(branch);

        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _context.Set<Branch>()
            .AnyAsync(branch => branch.Id == id);
    }
}