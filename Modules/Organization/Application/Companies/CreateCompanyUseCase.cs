using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Organization.Api.Contracts;
using ShiftManagement.Api.Modules.Organization.Domain;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Organization.Application.Companies;

public sealed class CreateCompanyUseCase(
    CompanyRepository companyRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<CompanyResponse>> ExecuteAsync(
        CreateCompanyRequest request
    )
    {
        var company = new Company(
            Guid.NewGuid(),
            request.Name,
            CompanyStatus.Active,
            DateTime.UtcNow
        );

        await companyRepository.AddAsync(company);

        await context.SaveChangesAsync();

        return Result<CompanyResponse>.Success(
            CompanyMapper.ToResponse(company)
        );
    }
}