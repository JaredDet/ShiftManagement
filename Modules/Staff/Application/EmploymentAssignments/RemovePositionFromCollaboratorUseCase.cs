using ShiftManagement.Api.Shared;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.PositionAssignments;
using ShiftManagement.Api.Modules.Staff.Application.Errors;

namespace ShiftManagement.Api.Modules.Staff.Application.EmploymentAssignments;

public sealed class RemovePositionFromCollaboratorUseCase(
    EmployeeRepository employeeRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result> Execute(
        Guid employeeId,
        RemovePositionFromCollaboratorRequest request
    )
    {
        var employee = await employeeRepository.GetByIdAsync(employeeId);

        if (employee is null)
            return Result.Failure(StaffErrors.EmployeeNotFound);

        var result = employee.RemoveAssignment(
            request.PositionId,
            AssignmentType.Position
        );

        if (!result.IsSuccess)
            return result;

        await context.SaveChangesAsync();

        return Result.Success();
    }
}