using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;
using ShiftManagement.Api.Modules.Staff.Application.Projections;

namespace ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;

public class CollaboratorReadRepository
{
    private readonly ShiftManagementDbContext _context;

    public CollaboratorReadRepository(ShiftManagementDbContext context)
    {
        _context = context;
    }

    public Task<List<CollaboratorResponse>> ListAsync(Guid companyId)
    {
        return _context.ToCollaboratorResponse()
            .Where(x => x.CompanyId == companyId)
            .ToListAsync();
    }

    public Task<CollaboratorResponse?> GetByIdAsync(Guid companyId, Guid id)
    {
        return _context.ToCollaboratorResponse()
            .FirstOrDefaultAsync(x => x.CompanyId == companyId && x.Id == id);
    }
}