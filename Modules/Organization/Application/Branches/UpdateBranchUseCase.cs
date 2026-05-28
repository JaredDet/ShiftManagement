using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Organization.Api.Contracts.Branches;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

namespace ShiftManagement.Api.Modules.Organization.Application.Branches;

public sealed class UpdateBranchUseCase(
    BranchRepository branchRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<BranchResponse>> ExecuteAsync(
        Guid id,
        UpdateBranchRequest request
    )
    {
        var branch = await branchRepository.GetByIdAsync(id);

        if (branch is null)
        {
            return Result<BranchResponse>.Failure(
                OrganizationErrors.BranchNotFound
            );
        }

        var nameNormalized = request.Name.Trim().ToLowerInvariant();

        var existsWithSameName = await branchRepository
            .ExistsByNameAndCompanyAsync(nameNormalized, branch.CompanyId);

        if (existsWithSameName &&
            !branch.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase))
        {
            return Result<BranchResponse>.Failure(
                OrganizationErrors.BranchAlreadyExists
            );
        }

        branch.Update(
            request.Name,
            request.Address
        );

        await context.SaveChangesAsync();

        return Result<BranchResponse>.Success(
            BranchMapper.ToResponse(branch)
        );
    }
}