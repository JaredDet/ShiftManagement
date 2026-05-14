using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Organization.Api.Contracts;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Organization.Application.Companies;

public sealed class UpdateCompanyUseCase(
    CompanyRepository companyRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<CompanyResponse>> ExecuteAsync(
        Guid id,
        UpdateCompanyRequest request
    )
    {
        var company = await companyRepository.GetByIdAsync(id);

        if (company is null)
        {
            return Result<CompanyResponse>.Failure(
                OrganizationErrors.CompanyNotFound
            );
        }

        company.Update(request.Name);

        await context.SaveChangesAsync();

        return Result<CompanyResponse>.Success(
            CompanyMapper.ToResponse(company)
        );
    }
}