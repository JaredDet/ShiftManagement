using ShiftManagement.Api.Shared;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.MainChanges;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Application.Mappers;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;
using ShiftManagement.Api.Infrastructure;

namespace ShiftManagement.Api.Modules.Staff.Application.EmploymentAssignments;

public sealed class ChangeMainBranchUseCase(
    EmployeeRepository employeeRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<CollaboratorResponse>> Execute(
        Guid employeeId,
        ChangeMainBranchRequest request
    )
    {
        var employee = await employeeRepository.GetByIdAsync(employeeId);

        if (employee is null)
            return Result<CollaboratorResponse>.Failure(StaffErrors.EmployeeNotFound);

        var result = employee.SetPrimary(
            request.BranchId,
            AssignmentType.Branch
        );

        if (!result.IsSuccess)
            return Result<CollaboratorResponse>.Failure(result.Error!);

        await context.SaveChangesAsync();

        return Result<CollaboratorResponse>.Success(
            EmployeeMapper.ToResponse(employee)
        );
    }
}