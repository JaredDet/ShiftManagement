using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Infrastructure.Persistence;

namespace ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;

public class CollaboratorRepository(ShiftManagementDbContext context)
{

    public Task<Collaborator?> GetByUserAndCompanyAsync(Guid userId, Guid companyId)
    {
        return context.Set<Collaborator>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.CompanyId == companyId
            );
    }

    public async Task AddAsync(Collaborator collaborator)
    {
        await context.Set<Collaborator>().AddAsync(collaborator);
    }

    public void Update(Collaborator collaborator)
    {
        context.Set<Collaborator>().Update(collaborator);
    }

    public void Remove(Collaborator collaborator)
    {
        context.Set<Collaborator>().Remove(collaborator);
    }

    public Task<Collaborator?> GetByIdAsync(Guid id)
    {
        return context.Set<Collaborator>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return context.Collaborators
            .AnyAsync(c => c.Id == id);
    }
}