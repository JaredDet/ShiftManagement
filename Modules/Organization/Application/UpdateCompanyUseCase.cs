using ShiftManagement.Api.Modules.Organization.Api.Contracts;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Organization.Application.UseCases;

public sealed class UpdateCompanyUseCase(
    CompanyRepository companyRepository
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

        await companyRepository.UpdateAsync(company);

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