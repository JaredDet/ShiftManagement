using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Identity.Domain;

namespace ShiftManagement.Api.Modules.Identity.Infrastructure;

public class UserRoleRepository
{
    private readonly ShiftManagementDbContext _context;

    public UserRoleRepository(ShiftManagementDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserRole userRole)
    {
        await _context.Set<UserRole>().AddAsync(userRole);
    }

    public Task<UserRole?> GetByIdAsync(Guid id)
    {
        return _context.Set<UserRole>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<UserRole>> GetByUserIdAsync(Guid userId)
    {
        return _context.Set<UserRole>()
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public Task<UserRole?> GetAsync(
        Guid userId,
        Role role,
        Guid? branchId
    )
    {
        return _context.Set<UserRole>()
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.Role == role &&
                x.BranchId == branchId
            );
    }

    public Task<bool> ExistsAsync(
        Guid userId,
        Role role,
        Guid? branchId
    )
    {
        return _context.Set<UserRole>()
            .AnyAsync(x =>
                x.UserId == userId &&
                x.Role == role &&
                x.BranchId == branchId
            );
    }

    public Task<bool> UserHasRoleAsync(Guid userId, Role role)
    {
        return _context.Set<UserRole>()
            .AnyAsync(x =>
                x.UserId == userId &&
                x.Role == role
            );
    }
}