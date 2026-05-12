using ShiftManagement.Api.Modules.Organization.Api.Contracts;
using ShiftManagement.Api.Modules.Organization.Domain;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Organization.Application.UseCases;

public sealed class CreateCompanyUseCase(
    CompanyRepository companyRepository
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

        await companyRepository.SaveChangesAsync();

        var response = new CompanyResponse(
            company.Id,
            company.Name,
            company.Status.ToString().ToLower(),
            company.CreatedAt
        );

        return Result<CompanyResponse>.Success(response);
    }
}