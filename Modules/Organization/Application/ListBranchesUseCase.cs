using ShiftManagement.Api.Modules.Organization.Api.Contracts;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Organization.Application.UseCases;

public sealed class ListBranchesUseCase(
    BranchRepository branchRepository
)
{
    public async Task<Result<BranchListResponse>> ExecuteAsync(
        Guid companyId
    )
    {
        var branches = await branchRepository
            .GetByCompanyIdAsync(companyId);

        var response = new BranchListResponse(
            branches
                .Select(branch => new BranchResponse(
                    branch.Id,
                    branch.CompanyId,
                    branch.Name,
                    branch.Address,
                    branch.Status.ToString().ToLower(),
                    branch.CreatedAt
                ))
                .ToList()
        );

        return Result<BranchListResponse>.Success(response);
    }
}