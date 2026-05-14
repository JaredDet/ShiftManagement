using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Identity.Domain;

namespace ShiftManagement.Api.Modules.Identity.Repository;

public sealed class UserCredentialRepository
{
    private readonly ShiftManagementDbContext _context;

    public UserCredentialRepository(
        ShiftManagementDbContext context
    )
    {
        _context = context;
    }

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