using ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;
using ShiftManagement.Api.Modules.Claims.Domain;
using ShiftManagement.Api.Modules.Claims.Infrastructure;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Staff.Infrastructure;



using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Claims.Application.Commands.Submissions;

public sealed class CreateClaimUseCase(
    ClaimRepository claimRepository,
    CompanyRepository companyRepository,
    CollaboratorRepository CollaboratorRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<ClaimResponse>> ExecuteAsync(
        CreateClaimRequest request)
    {
        var companyExists =
            await companyRepository.ExistsAsync(
                request.CompanyId);

        if (!companyExists)
        {
            return Result<ClaimResponse>.Failure(
                OrganizationErrors.CompanyNotFound);
        }

        var collaboratorExists =
            await CollaboratorRepository.ExistsAsync(
                request.CollaboratorId);

        if (!collaboratorExists)
        {
            return Result<ClaimResponse>.Failure(
                StaffErrors.CollaboratorNotFound);
        }

        var claim = Claim.Create(
            request.CompanyId,
            request.CollaboratorId,
            request.Reason,
            request.Priority,
            request.Title,
            request.Description
        );

        await claimRepository.AddAsync(claim);
        await context.SaveChangesAsync();

        return Result<ClaimResponse>.Success(
            ClaimMapper.ToResponse(claim)
        );
    }
}