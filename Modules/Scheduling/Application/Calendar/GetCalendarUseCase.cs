using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Infrastructure;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Scheduling.Application.Calendar;

public sealed class GetCalendarUseCase(
    CalendarReadRepository repository
)
{
    public async Task<Result<List<CalendarResponse>>> ExecuteAsync(CalendarRequest request)
    {
        var calendar = await repository.GetAsync(request.StartsAt, request.EndsAt);

        return Result<List<CalendarResponse>>.Success(calendar);
    }
}