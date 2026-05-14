using ShiftManagement.Api.Shared;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Application.Mappers;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Identity.Repository;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Identity.Application;

namespace ShiftManagement.Api.Modules.Staff.Application.Collaborators;

public sealed class CreateCollaboratorUseCase(
    EmployeeRepository employeeRepository,
    CompanyRepository companyRepository,
    UserRepository userRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<CollaboratorResponse>> Execute(CreateCollaboratorRequest request)
    {
        var validation = await ValidateRequest(request);
        if (!validation.IsSuccess)
            return Result<CollaboratorResponse>.Failure(validation.Error!);

        var employee = Employee.Create(request.UserId, request.CompanyId);

        var branchResult = employee.AddAssignment(
            request.MainBranchId,
            AssignmentType.Branch,
            isPrimary: true
        );

        if (!branchResult.IsSuccess)
            return Result<CollaboratorResponse>.Failure(branchResult.Error!);

        var positionResult = employee.AddAssignment(
            request.MainPositionId,
            AssignmentType.Position,
            isPrimary: true
        );

        if (!positionResult.IsSuccess)
            return Result<CollaboratorResponse>.Failure(positionResult.Error!);

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

        return Result.Success();
    }
}