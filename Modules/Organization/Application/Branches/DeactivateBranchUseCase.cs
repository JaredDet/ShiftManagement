using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Organization.Application.Branches;

public sealed class DeactivateBranchUseCase(
    BranchRepository branchRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result> ExecuteAsync(Guid id)
    {
        var branch = await branchRepository.GetByIdAsync(id);

        if (branch is null)
            return Result.Failure(
                OrganizationErrors.BranchNotFound
            );

        branch.Deactivate();

        await context.SaveChangesAsync();

        return Result.Success();
    }
}