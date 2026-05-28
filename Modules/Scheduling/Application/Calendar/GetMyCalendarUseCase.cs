using ShiftManagement.Api.BuildingBlocks.Execution;
using ShiftManagement.Api.BuildingBlocks.Results;

using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Infrastructure;

using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;

namespace ShiftManagement.Api.Modules.Scheduling.Application.Calendar;

public sealed class GetMyCalendarUseCase(
    CalendarReadRepository repository,
    EmployeeRepository employeeRepository,
    IExecutionContext context
)
{
    public async Task<Result<List<CalendarResponse>>> ExecuteAsync(
        CalendarRequest request
    )
    {
        var employee = await employeeRepository
            .GetByUserAndCompanyAsync(
                context.UserId,
                context.CompanyId
            );

        if (employee is null)
        {
            return Result<List<CalendarResponse>>.Failure(
                StaffErrors.EmployeeNotFound
            );
        }

        var calendar = await repository.GetByCollaboratorAsync(
            employee.Id,
            request.StartsAt,
            request.EndsAt
        );

        return Result<List<CalendarResponse>>
            .Success(calendar);
    }
}