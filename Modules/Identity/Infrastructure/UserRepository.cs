using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Identity.Domain;

namespace ShiftManagement.Api.Modules.Identity.Infrastructure;

public class UserRepository(ShiftManagementDbContext context)
{

    public Task<bool> ExistsAsync(Guid userId)
    {
        return context.Set<User>()
            .AnyAsync(x => x.Id == userId && x.Status == UserStatus.Active);
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        return context.Set<User>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(User user)
    {
        await context.Set<User>().AddAsync(user);
    }

    public async Task<User?> GetByEmailAsync(
    string email
)
    {
        return await context.Set<User>()
            .FirstOrDefaultAsync(
                x => x.Email == email.Trim().ToLowerInvariant()
            );
    }
}