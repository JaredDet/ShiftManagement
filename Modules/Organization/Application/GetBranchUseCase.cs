using ShiftManagement.Api.Modules.Organization.Api.Contracts;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Organization.Application.UseCases;

public sealed class GetBranchUseCase(
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