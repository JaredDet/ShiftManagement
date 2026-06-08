using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Identity.Domain;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Domain;
using ShiftManagement.Api.Modules.Staff.Domain;

namespace ShiftManagement.Api.Modules.Scheduling.Application.Swaps;

public static class ShiftSwapProjections
{
    public static IQueryable<ShiftSwapRequestResponse> ToShiftSwapResponse(
    this ShiftManagementDbContext context
)
    {
        var swaps = context.Set<ShiftSwapRequest>().AsNoTracking();

        var collaborators = context.Set<Collaborator>().AsNoTracking();
        var users = context.Set<User>().AsNoTracking();

        return
            from s in swaps

            join r in collaborators
                on s.RequesterId equals r.Id

            join ru in users
                on r.UserId equals ru.Id

            join t in collaborators
                on s.TargetCollaboratorId equals t.Id

            join tu in users
                on t.UserId equals tu.Id

            select new ShiftSwapRequestResponse
            {
                Id = s.Id,

                RequesterId = s.RequesterId,
                RequesterName = ru.Name,

                TargetCollaboratorId = s.TargetCollaboratorId,
                TargetCollaboratorName = tu.Name,

                SourceShiftId = s.SourceShiftId,
                TargetShiftId = s.TargetShiftId,

                Status = s.Status.ToString(),

                CreatedAt = s.CreatedAt,
                RespondedAt = s.RespondedAt,
                ApprovedAt = s.ApprovedAt
            };
    }
}