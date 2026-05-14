using ShiftManagement.Api.Modules.Organization.Api.Contracts.Companies;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Organization.Application.Companies;

public sealed class GetCompanyUseCase(
    CompanyRepository companyRepository
)
{
    public async Task<Result<CompanyResponse>> ExecuteAsync(Guid id)
    {
        var company = await companyRepository.GetByIdAsync(id);

        if (company is null)
        {
            return Result<CompanyResponse>.Failure(
                OrganizationErrors.CompanyNotFound
            );
        }

        return Result<CompanyResponse>.Success(
            CompanyMapper.ToResponse(company)
        );
    }
}