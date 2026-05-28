using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Identity.Domain;

namespace ShiftManagement.Api.Modules.Identity.Infrastructure;

public class UserRoleRepository(ShiftManagementDbContext context)
{

    public async Task AddAsync(UserRole userRole)
    {
        await context.Set<UserRole>().AddAsync(userRole);
    }

    public Task<UserRole?> GetByIdAsync(Guid id)
    {
        return context.Set<UserRole>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<UserRole>> GetByUserIdAsync(Guid userId)
    {
        return context.Set<UserRole>()
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public Task<UserRole?> GetAsync(
        Guid userId,
        Role role,
        Guid? branchId
    )
    {
        return context.Set<UserRole>()
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.Role == role &&
                x.BranchId == branchId
            );
    }

    public Task<List<UserRole>> GetActiveByUserIdAsync(Guid userId)
    {
        return context.UserRoles
            .Where(role =>
                role.UserId == userId &&
                role.Status == UserRoleStatus.Active
            )
            .ToListAsync();
    }

    public Task<bool> ExistsAsync(
        Guid userId,
        Role role,
        Guid? branchId
    )
    {
        return context.Set<UserRole>()
            .AnyAsync(x =>
                x.UserId == userId &&
                x.Role == role &&
                x.BranchId == branchId
            );
    }

    public Task<bool> UserHasRoleAsync(Guid userId, Role role)
    {
        return context.Set<UserRole>()
            .AnyAsync(x =>
                x.UserId == userId &&
                x.Role == role
            );
    }
}