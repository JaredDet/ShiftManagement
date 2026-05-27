using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Queries;
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

    public static IQueryable<ClaimResponse> ApplyFilters(
        this IQueryable<ClaimResponse> query,
        ListClaimsRequest request
    )
    {
        if (request.CollaboratorId.HasValue)
        {
            query = query.Where(x =>
                x.CollaboratorId == request.CollaboratorId.Value
            );
        }

        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            query = query.Where(x =>
                x.Status == request.Status
            );
        }

        if (!string.IsNullOrWhiteSpace(request.Reason))
        {
            query = query.Where(x =>
                x.Reason == request.Reason
            );
        }

        if (!string.IsNullOrWhiteSpace(request.Priority))
        {
            query = query.Where(x =>
                x.Priority == request.Priority
            );
        }

        if (request.AssignedToUserId.HasValue)
        {
            query = query.Where(x =>
                x.AssignedToUserId == request.AssignedToUserId.Value
            );
        }

        if (request.CreatedFrom.HasValue)
        {
            query = query.Where(x =>
                x.CreatedAt >= request.CreatedFrom.Value
            );
        }

        if (request.CreatedTo.HasValue)
        {
            query = query.Where(x =>
                x.CreatedAt <= request.CreatedTo.Value
            );
        }

        return query;
    }
}