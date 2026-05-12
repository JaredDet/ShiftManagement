using ShiftManagement.Api.Modules.Organization.Api.Contracts;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Organization.Application.UseCases;

public sealed class DeactivateBranchUseCase(
    BranchRepository branchRepository
)
{
    public async Task<Result<BranchResponse>> ExecuteAsync(Guid id)
    {
        var branch = await branchRepository.GetByIdAsync(id);

        if (branch is null)
        {
            return Result<BranchResponse>.Failure(
                OrganizationErrors.BranchNotFound
            );
        }

        branch.Deactivate();

        await branchRepository.UpdateAsync(branch);

        await branchRepository.SaveChangesAsync();

        var response = new BranchResponse(
            branch.Id,
            branch.CompanyId,
            branch.Name,
            branch.Address,
            branch.Status.ToString().ToLower(),
            branch.CreatedAt
        );

        return Result<BranchResponse>.Success(response);
    }
}