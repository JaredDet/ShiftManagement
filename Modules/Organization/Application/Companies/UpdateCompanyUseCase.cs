using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Organization.Api.Contracts.Companies;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

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

        var nameNormalized = request.Name.Trim().ToLowerInvariant();

        var existsWithSameName = await companyRepository.ExistsByNameAsync(nameNormalized);

        if (existsWithSameName && !string.Equals(company.Name, request.Name, StringComparison.OrdinalIgnoreCase))
        {
            return Result<CompanyResponse>.Failure(
                OrganizationErrors.CompanyAlreadyExists
            );
        }

        company.Update(request.Name);

        await context.SaveChangesAsync();

        return Result<CompanyResponse>.Success(
            CompanyMapper.ToResponse(company)
        );
    }
}