using ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;
using ShiftManagement.Api.Modules.Staff.Domain;

namespace ShiftManagement.Api.Modules.Staff.Infrastructure.Projections;

public static class PositionProjection
{
    public static IQueryable<PositionResponse> ToPositionResponse(this IQueryable<Position> query)
    {
        return query.Select(p => new PositionResponse
        {
            Id = p.Id,
            CompanyId = p.CompanyId,
            Name = p.Name,
            Description = p.Description,
            Status = p.Status.ToString(),
            CreatedAt = p.CreatedAt
        });
    }
}