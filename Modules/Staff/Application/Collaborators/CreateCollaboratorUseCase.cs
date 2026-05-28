using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Application.Mappers;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Identity.Infrastructure;
using ShiftManagement.Api.Modules.Identity.Application;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Staff.Application.Collaborators;

public sealed class CreateCollaboratorUseCase(
    EmployeeRepository employeeRepository,
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

        var employee = Employee.Create(
            request.UserId,
            request.CompanyId
        );

        employee.AddAssignment(
            request.MainBranchId,
            AssignmentType.Branch,
            isPrimary: true
        );

        employee.AddAssignment(
            request.MainPositionId,
            AssignmentType.Position,
            isPrimary: true
        );

        await employeeRepository.AddAsync(employee);

        await context.SaveChangesAsync();

        return Result<CollaboratorResponse>.Success(
            EmployeeMapper.ToResponse(employee)
        );
    }

    private async Task<Result> ValidateRequest(CreateCollaboratorRequest request)
    {
        if (!await userRepository.ExistsAsync(request.UserId))
            return Result.Failure(IdentityErrors.UserNotFound);

        if (!await companyRepository.ExistsAsync(request.CompanyId))
            return Result.Failure(OrganizationErrors.CompanyNotFound);

        var existing = await employeeRepository.GetByUserAndCompanyAsync(
            request.UserId,
            request.CompanyId
        );

        if (existing is not null)
            return Result.Failure(StaffErrors.EmployeeAlreadyExists);

        var hasAccess = await staffAccessPolicy.CanAccess(request.UserId);

        if (!hasAccess)
            return Result.Failure(StaffErrors.UserNotEligibleForEmployee);

        return Result.Success();
    }
}