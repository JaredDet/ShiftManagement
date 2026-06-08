using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Infrastructure.Persistence;

namespace ShiftManagement.Api.Modules.Staff.Infrastructure;

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

    public Task<Collaborator?> GetByIdAsync(Guid id)
    {
        return context.Set<Collaborator>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Collaborator?> GetByIdAsync(Guid companyId, Guid id)
    {
        return context.Set<Collaborator>()
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.CompanyId == companyId
            );
    }

    public Task<bool> HasBranchAccessAsync(
    Guid userId,
    Guid companyId,
    Guid branchId
)
    {
        return context.Set<Collaborator>()
            .Where(x => x.UserId == userId && x.CompanyId == companyId)
            .SelectMany(x => x.Assignments)
            .AnyAsync(a =>
                a.Type == AssignmentType.Branch &&
                a.ReferenceId == branchId &&
                a.Status == AssignmentStatus.Active
            );
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return context.Collaborators
            .AnyAsync(c => c.Id == id);
    }
}