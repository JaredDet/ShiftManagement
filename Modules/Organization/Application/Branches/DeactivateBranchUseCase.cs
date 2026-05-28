using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

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