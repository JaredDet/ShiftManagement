using ShiftManagement.Api.Modules.Organization.Api.Contracts;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Organization.Application.Companies;

public sealed class ListCompaniesUseCase(
    CompanyRepository companyRepository
)
{
    public async Task<Result<CompanyListResponse>> ExecuteAsync()
    {
        var companies = await companyRepository.ListAsync();

        return Result<CompanyListResponse>.Success(
            new CompanyListResponse(
                [.. companies.Select(CompanyMapper.ToResponse)]
            )
        );
    }
}