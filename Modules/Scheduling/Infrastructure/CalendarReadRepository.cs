using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Application.Calendar;

namespace ShiftManagement.Api.Modules.Scheduling.Infrastructure;

public class CalendarReadRepository
{
    private readonly ShiftManagementDbContext _context;

    public CalendarReadRepository(ShiftManagementDbContext context)
    {
        _context = context;
    }

    public Task<List<CalendarResponse>> GetAsync(DateTime start, DateTime end)
    {
        return _context.ToCalendarResponse()
            .Where(x => x.StartsAt >= start && x.EndsAt <= end)
            .ToListAsync();
    }
}