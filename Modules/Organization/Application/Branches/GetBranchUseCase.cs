using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Organization.Api.Contracts.Branches;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

namespace ShiftManagement.Api.Modules.Organization.Application.Branches;

public sealed class GetBranchUseCase(
    BranchRepository branchRepository
)
{
    public async Task<Result<BranchResponse>> ExecuteAsync(Guid id)
    {
        var branch = await branchRepository.GetByIdAsync(id);

        if (branch is null)
        {
            return Result<BranchResponse>.Failure(
                OrganizationErrors.BranchNotFound
            );
        }

        return Result<BranchResponse>.Success(
            BranchMapper.ToResponse(branch)
        );
    }
}