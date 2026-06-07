using ShiftManagement.Api.BuildingBlocks.Execution;
using ShiftManagement.Api.BuildingBlocks.Results;

using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Infrastructure;

using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Infrastructure;




namespace ShiftManagement.Api.Modules.Scheduling.Application.Calendar;

public sealed class GetMyCalendarUseCase(
    CalendarReadRepository repository,
    CollaboratorRepository CollaboratorRepository,
    IExecutionContext context
)
{
    public async Task<Result<List<CalendarResponse>>> ExecuteAsync(
        CalendarRequest request
    )
    {
        var collaborator = await CollaboratorRepository
            .GetByUserAndCompanyAsync(
                context.UserId,
                context.CompanyId
            );

        if (collaborator is null)
        {
            return Result<List<CalendarResponse>>.Failure(
                StaffErrors.CollaboratorNotFound
            );
        }

        var calendar = await repository.GetByCollaboratorAsync(
            collaborator.Id,
            request.StartsAt,
            request.EndsAt
        );

        return Result<List<CalendarResponse>>
            .Success(calendar);
    }
}