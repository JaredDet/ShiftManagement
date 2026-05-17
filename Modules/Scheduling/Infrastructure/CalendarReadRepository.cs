using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Application.Calendar;

namespace ShiftManagement.Api.Modules.Scheduling.Infrastructure;

public class CalendarReadRepository(ShiftManagementDbContext context)
{

    public Task<List<CalendarResponse>> GetAsync(DateTime start, DateTime end)
    {
        return context.ToCalendarResponse()
            .Where(x => x.StartsAt >= start && x.EndsAt <= end)
            .ToListAsync();
    }
}