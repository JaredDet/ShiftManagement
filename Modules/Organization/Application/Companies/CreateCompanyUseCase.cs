using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Organization.Api.Contracts.Companies;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
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
        var nameNormalized = request.Name.Trim().ToLowerInvariant();

        var exists = await companyRepository.ExistsByNameAsync(nameNormalized);

        if (exists)
            return Result<CompanyResponse>.Failure(
                OrganizationErrors.CompanyAlreadyExists
            );

        var company = Company.Create(
            request.Name
        );

        await companyRepository.AddAsync(company);

        await context.SaveChangesAsync();

        return Result<CompanyResponse>.Success(
            CompanyMapper.ToResponse(company)
        );
    }
}