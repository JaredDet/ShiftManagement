using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Shared;
using ShiftManagement.Api.Modules.Organization.Domain;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Organization.Api.Contracts.Branches;

namespace ShiftManagement.Api.Modules.Organization.Application.Branches;

public sealed class CreateBranchUseCase(
    BranchRepository branchRepository,
    CompanyRepository companyRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<BranchResponse>> ExecuteAsync(
    CreateBranchRequest request
)
    {
        var company = await companyRepository.GetByIdAsync(request.CompanyId);

        if (company is null)
        {
            return Result<BranchResponse>.Failure(
                OrganizationErrors.CompanyNotFound
            );
        }

        var nameNormalized = request.Name.Trim().ToLowerInvariant();

        var exists = await branchRepository.ExistsByNameAndCompanyAsync(
            nameNormalized,
            request.CompanyId
        );

        if (exists)
        {
            return Result<BranchResponse>.Failure(
                OrganizationErrors.BranchAlreadyExists
            );
        }

        var branch = Branch.Create(
            request.CompanyId,
            request.Name,
            request.Address
        );

        await branchRepository.AddAsync(branch);

        await context.SaveChangesAsync();

        return Result<BranchResponse>.Success(
            BranchMapper.ToResponse(branch)
        );
    }
}