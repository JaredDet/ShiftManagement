using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

namespace ShiftManagement.Api.Modules.Organization.Application.Companies;

public sealed class DeactivateCompanyUseCase(
    CompanyRepository companyRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result> ExecuteAsync(Guid id)
    {
        var company = await companyRepository.GetByIdAsync(id);

        if (company is null)
            return Result.Failure(OrganizationErrors.CompanyNotFound);

        company.Deactivate();

        await context.SaveChangesAsync();

        return Result.Success();
    }
}