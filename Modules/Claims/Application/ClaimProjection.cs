using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;
using ShiftManagement.Api.Modules.Claims.Domain;

namespace ShiftManagement.Api.Modules.Claims.Application;

public static class ClaimProjection
{
    public static IQueryable<ClaimResponse> ToClaimResponse(
        this DbContext context)
    {
        var claims = context.Set<Claim>().AsNoTracking();
        var comments = context.Set<ClaimComment>().AsNoTracking();

        var query =
            from c in claims

            select new ClaimResponse
            {
                Id = c.Id,
                CompanyId = c.CompanyId,
                CollaboratorId = c.CollaboratorId,

                Reason = c.Reason.ToString(),
                Priority = c.Priority.ToString(),
                Status = c.Status.ToString(),

                Title = c.Title,
                Description = c.Description,

                AssignedToUserId = c.AssignedToUserId,

                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                ResolvedAt = c.ResolvedAt,
                CanceledAt = c.CanceledAt,

                CommentsCount =
                    comments.Count(x => x.ClaimId == c.Id)
            };

        return query;
    }
}