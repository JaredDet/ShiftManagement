using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Contracts.Domain;

namespace ShiftManagement.Api.Modules.Contracts.Infrastructure;

public sealed class ContractRepository(
    ShiftManagementDbContext context
)
{
    public async Task AddAsync(EmploymentContract contract)
    {
        await context.EmploymentContracts.AddAsync(contract);
    }

    public async Task<EmploymentContract?> GetByIdAsync(Guid id)
    {
        return await context.EmploymentContracts
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}