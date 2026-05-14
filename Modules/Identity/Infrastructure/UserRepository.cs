using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Identity.Domain;

namespace ShiftManagement.Api.Modules.Identity.Repository;

public class UserRepository
{
    private readonly ShiftManagementDbContext _context;

    public UserRepository(ShiftManagementDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExistsAsync(Guid userId)
    {
        return _context.Set<User>()
            .AnyAsync(x => x.Id == userId && x.Status == UserStatus.Active);
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        return _context.Set<User>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(User user)
    {
        await _context.Set<User>().AddAsync(user);
    }

    public async Task<User?> GetByEmailAsync(
    string email
)
    {
        return await _context.Set<User>()
            .FirstOrDefaultAsync(
                x => x.Email == email.Trim().ToLowerInvariant()
            );
    }
}