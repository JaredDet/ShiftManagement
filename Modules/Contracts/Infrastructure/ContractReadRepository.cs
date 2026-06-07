using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Contracts.Api.Contracts.Queries;
using ShiftManagement.Api.Modules.Contracts.Api.Contracts.Query;
using ShiftManagement.Api.Modules.Contracts.Domain;
using ShiftManagement.Api.Modules.Contracts.Domain.Enums;

namespace ShiftManagement.Api.Modules.Contracts.Infrastructure;

public sealed class ContractReadRepository(
    ShiftManagementDbContext context
)
{
    public async Task<EmploymentContractResponse?> GetByIdAsync(Guid id, Guid? collaboratorId = null)
    {
        return await context
            .ToContractResponse(contractId: id, collaboratorId: collaboratorId)
            .FirstOrDefaultAsync();
    }

    public async Task<List<EmploymentContractResponse>> ListAsync(
        Guid? collaboratorId = null, Guid? contractId = null)
    {
        return await context
            .ToContractResponse(contractId: contractId, collaboratorId: collaboratorId)
            .ToListAsync();
    }

    public async Task<EmploymentContractResponse?> GetActiveByCollaboratorAsync(Guid collaboratorId)
    {
        return await context
            .ToContractResponse(collaboratorId: collaboratorId)
            .Where(x => x.Status == ContractStatus.ACTIVE.ToString())
            .FirstOrDefaultAsync();
    }

    public async Task<List<EmploymentContractResponse>> ListByCollaboratorAsync(
    Guid collaboratorId)
    {
        return await context
            .ToContractResponse(collaboratorId: collaboratorId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<MyEmploymentContractResponse?> GetMyContractAsync(Guid userId)
    {
        return await context
            .ToMyContractResponse(userId)
            .FirstOrDefaultAsync();
    }
}