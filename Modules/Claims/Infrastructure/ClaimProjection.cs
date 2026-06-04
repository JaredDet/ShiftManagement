using Microsoft.EntityFrameworkCore;

using ShiftManagement.Api.Modules.Claims.Api.Contracts.Queries;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;
using ShiftManagement.Api.Modules.Claims.Domain;

namespace ShiftManagement.Api.Modules.Claims.Infrastructure;

public static class ClaimProjection
{
    public static IQueryable<ClaimResponse> ToClaimResponse(
        this DbContext context
    )
    {
        var claims = context
            .Set<Claim>()
            .AsNoTracking();

        var comments = context
            .Set<ClaimComment>()
            .AsNoTracking();

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
                x.CollaboratorId ==
                request.CollaboratorId.Value
            );
        }

        if (request.Status.HasValue)
        {
            var status =
                request.Status.Value.ToString();

            query = query.Where(x =>
                x.Status == status
            );
        }

        if (request.Reason.HasValue)
        {
            var reason =
                request.Reason.Value.ToString();

            query = query.Where(x =>
                x.Reason == reason
            );
        }

        if (request.Priority.HasValue)
        {
            var priority =
                request.Priority.Value.ToString();

            query = query.Where(x =>
                x.Priority == priority
            );
        }

        if (request.AssignedToUserId.HasValue)
        {
            query = query.Where(x =>
                x.AssignedToUserId ==
                request.AssignedToUserId.Value
            );
        }

        if (request.CreatedFrom.HasValue)
        {
            query = query.Where(x =>
                x.CreatedAt >=
                request.CreatedFrom.Value
            );
        }

        if (request.CreatedTo.HasValue)
        {
            query = query.Where(x =>
                x.CreatedAt <=
                request.CreatedTo.Value
            );
        }

        return query;
    }
}