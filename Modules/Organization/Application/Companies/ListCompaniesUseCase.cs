using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Organization.Api.Contracts.Companies;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

namespace ShiftManagement.Api.Modules.Organization.Application.Companies;

public sealed class ListCompaniesUseCase(
    CompanyRepository companyRepository
)
{
    public async Task<Result<List<CompanyResponse>>> ExecuteAsync()
    {
        var companies = await companyRepository.ListAsync();

        return Result<List<CompanyResponse>>.Success(
            [.. companies.Select(CompanyMapper.ToResponse)]
        );
    }
}