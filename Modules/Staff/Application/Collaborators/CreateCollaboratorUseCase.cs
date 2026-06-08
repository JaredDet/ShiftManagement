using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Modules.Staff.Infrastructure;



using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Identity.Infrastructure;
using ShiftManagement.Api.Modules.Identity.Application;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Staff.Application.Collaborators;

public sealed class CreateCollaboratorUseCase(
    CollaboratorRepository CollaboratorRepository,
    CollaboratorReadRepository collaboratorReadRepository,
    CompanyRepository companyRepository,
    UserRepository userRepository,
    StaffAccessPolicy staffAccessPolicy,
    ShiftManagementDbContext context
)
{
    public async Task<Result<CollaboratorResponse>> Execute(
    CreateCollaboratorRequest request
)
    {
        var validation = await ValidateRequest(request);

        if (!validation.IsSuccess)
        {
            return Result<CollaboratorResponse>
                .Failure(validation.Error!);
        }

        var collaborator = Collaborator.Create(
            request.UserId,
            request.CompanyId
        );

        collaborator.AddAssignment(
            request.MainBranchId,
            AssignmentType.Branch,
            isPrimary: true
        );

        collaborator.AddAssignment(
            request.MainPositionId,
            AssignmentType.Position,
            isPrimary: true
        );

        await CollaboratorRepository.AddAsync(collaborator);

        await context.SaveChangesAsync();

        var result = await collaboratorReadRepository.GetByIdAsync(collaborator.Id, request.CompanyId);

        if (result is null)
            return Result<CollaboratorResponse>.Failure(StaffErrors.CollaboratorNotFound);

        return Result<CollaboratorResponse>.Success(result);
    }

    private async Task<Result> ValidateRequest(CreateCollaboratorRequest request)
    {
        if (!await userRepository.ExistsAsync(request.UserId))
            return Result.Failure(IdentityErrors.UserNotFound);

        if (!await companyRepository.ExistsAsync(request.CompanyId))
            return Result.Failure(OrganizationErrors.CompanyNotFound);

        var existing = await CollaboratorRepository.GetByUserAndCompanyAsync(
            request.UserId,
            request.CompanyId
        );

        if (existing is not null)
            return Result.Failure(StaffErrors.CollaboratorAlreadyExists);

        var hasAccess = await staffAccessPolicy.CanAccess(request.UserId);

        if (!hasAccess)
            return Result.Failure(StaffErrors.UserNotEligibleForCollaborator);

        return Result.Success();
    }
}