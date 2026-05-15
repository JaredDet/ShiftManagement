using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Identity.Domain;

namespace ShiftManagement.Api.Modules.Identity.Infrastructure;

public sealed class UserCredentialRepository(
    ShiftManagementDbContext context
    )
{
    private readonly ShiftManagementDbContext _context = context;

    public async Task AddAsync(
        UserCredential credential
    )
    {
        await _context.Set<UserCredential>()
            .AddAsync(credential);
    }

    public async Task<UserCredential?> GetByUserIdAsync(
        Guid userId
    )
    {
        return await _context.Set<UserCredential>()
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }
}