using ShiftManagement.Api.Modules.Organization.Api.Contracts;

using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Shared;
using ShiftManagement.Api.Modules.Organization.Domain;

namespace ShiftManagement.Api.Modules.Organization.Application.UseCases;

public sealed class CreateBranchUseCase(
    BranchRepository branchRepository,
    CompanyRepository companyRepository
)
{
    public async Task<Result<BranchResponse>> ExecuteAsync(
        CreateBranchRequest request
    )
    {
        var company = await companyRepository
            .GetByIdAsync(request.CompanyId);

        if (company is null)
        {
            return Result<BranchResponse>.Failure(
                OrganizationErrors.CompanyNotFound
            );
        }

        var branch = new Branch(
            Guid.NewGuid(),
            request.CompanyId,
            request.Name,
            request.Address,
            BranchStatus.Active,
            DateTime.UtcNow
        );

        await branchRepository.AddAsync(branch);

        await branchRepository.SaveChangesAsync();

        var response = new BranchResponse(
            branch.Id,
            branch.CompanyId,
            branch.Name,
            branch.Address,
            branch.Status.ToString().ToLower(),
            branch.CreatedAt
        );

        return Result<BranchResponse>.Success(response);
    }
}