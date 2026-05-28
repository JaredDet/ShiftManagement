using Microsoft.EntityFrameworkCore;

using ShiftManagement.Api.Modules.Organization.Domain;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Domain;
using ShiftManagement.Api.Modules.Staff.Domain;

namespace ShiftManagement.Api.Modules.Scheduling.Application.Calendar;

public static class CalendarProjection
{
    public static IQueryable<CalendarResponse> ToCalendarResponse(
        this DbContext context
    )
    {
        var shifts = context.Set<Shift>().AsNoTracking();

        var assignments = context
            .Set<ShiftAssignment>()
            .AsNoTracking();

        var branches = context
            .Set<Branch>()
            .AsNoTracking();

        var positions = context
            .Set<Position>()
            .AsNoTracking();

        var query =
            from s in shifts

            join b in branches
                on s.BranchId equals b.Id

            join p in positions
                on s.PositionId equals p.Id

            join a in assignments
                on s.Id equals a.ShiftId into ag

            select new CalendarResponse
            {
                BranchId = s.BranchId,

                BranchName = b.Name,

                StartsAt = s.StartsAt,

                EndsAt = s.EndsAt,

                Shifts = ag
                    .Select(_ => new ShiftResponse
                    {
                        Id = s.Id,

                        BranchId = s.BranchId,

                        BranchName = b.Name,

                        PositionId = s.PositionId,

                        PositionName = p.Name,

                        StartsAt = s.StartsAt,

                        EndsAt = s.EndsAt,

                        Status = s.Status.ToString(),

                        CreatedAt = s.CreatedAt
                    })
                    .ToList()
            };

        return query;
    }

    public static IQueryable<CalendarResponse>
        ToCollaboratorCalendarResponse(
            this DbContext context,
            Guid collaboratorId
        )
    {
        var shifts = context.Set<Shift>().AsNoTracking();

        var assignments = context
            .Set<ShiftAssignment>()
            .AsNoTracking();

        var branches = context
            .Set<Branch>()
            .AsNoTracking();

        var positions = context
            .Set<Position>()
            .AsNoTracking();

        var query =
            from s in shifts

            join a in assignments
                on s.Id equals a.ShiftId

            join b in branches
                on s.BranchId equals b.Id

            join p in positions
                on s.PositionId equals p.Id

            where a.CollaboratorId == collaboratorId

            select new CalendarResponse
            {
                BranchId = s.BranchId,

                BranchName = b.Name,

                StartsAt = s.StartsAt,

                EndsAt = s.EndsAt,

                Shifts = new List<ShiftResponse>
                {
                    new ShiftResponse
                    {
                        Id = s.Id,

                        BranchId = s.BranchId,

                        BranchName = b.Name,

                        PositionId = s.PositionId,

                        PositionName = p.Name,

                        StartsAt = s.StartsAt,

                        EndsAt = s.EndsAt,

                        Status = s.Status.ToString(),

                        CreatedAt = s.CreatedAt
                    }
                }
            };

        return query;
    }
}