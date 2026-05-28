using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Organization.Api.Contracts.Branches;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

namespace ShiftManagement.Api.Modules.Organization.Application.Branches;

public sealed class ListBranchesUseCase(
    BranchRepository branchRepository,
    CompanyRepository companyRepository
)
{
    public async Task<Result<List<BranchResponse>>> ExecuteAsync(Guid companyId)
    {
        var companyExists = await companyRepository.ExistsAsync(companyId);

        if (!companyExists)
            return Result<List<BranchResponse>>.Failure(
                OrganizationErrors.CompanyNotFound
            );

        var branches = await branchRepository.GetByCompanyIdAsync(companyId);

        return Result<List<BranchResponse>>.Success(
            BranchMapper.ToResponseList(branches)
        );
    }
}