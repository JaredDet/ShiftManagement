using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Identity.Domain;

namespace ShiftManagement.Api.Modules.Identity.Infrastructure;

public sealed class UserCredentialRepository(
    ShiftManagementDbContext context
    )
{

    public async Task AddAsync(
        UserCredential credential
    )
    {
        await context.Set<UserCredential>()
            .AddAsync(credential);
    }

    public async Task<UserCredential?> GetByUserIdAsync(
        Guid userId
    )
    {
        return await context.Set<UserCredential>()
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }
}