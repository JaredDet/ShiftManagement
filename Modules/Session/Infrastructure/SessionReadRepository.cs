using ShiftManagement.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Session.Api.Contracts;

namespace ShiftManagement.Api.Modules.Session.Infrastructure;

public class SessionReadRepository(ShiftManagementDbContext context)
{
    public Task<SessionResponse?> GetContextAsync(Guid userId, Guid companyId)
    {
        return context.ToSessionContext(userId, companyId)
            .FirstOrDefaultAsync();
    }
}