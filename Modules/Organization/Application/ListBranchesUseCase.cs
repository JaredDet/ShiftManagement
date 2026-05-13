using ShiftManagement.Api.Modules.Organization.Api.Contracts;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Organization.Application.UseCases;

public sealed class ListBranchesUseCase(
    BranchRepository branchRepository,
    CompanyRepository companyRepository
)
{
    public async Task<Result<BranchListResponse>> ExecuteAsync(Guid companyId)
    {
        var companyExists = await companyRepository.ExistsAsync(companyId);

        if (!companyExists)
            return Result<BranchListResponse>.Failure(OrganizationErrors.CompanyNotFound);

        var branches = await branchRepository.GetByCompanyIdAsync(companyId);

        var response = new BranchListResponse(
            [.. branches.Select(branch => new BranchResponse(
                branch.Id,
                branch.CompanyId,
                branch.Name,
                branch.Address,
                branch.Status.ToString().ToLowerInvariant(),
                branch.CreatedAt
            ))]
        );

        return Result<BranchListResponse>.Success(response);
    }
}