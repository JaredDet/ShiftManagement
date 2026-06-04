using ShiftManagement.Api.Modules.Staff.Api.Contracts.PositionAssignments;
using ShiftManagement.Api.Modules.Staff.Domain;

namespace ShiftManagement.Api.Modules.Staff.Infrastructure.Projections;

public static class PositionAssignmentProjection
{
    public static IQueryable<CollaboratorPositionResponse> ToPositionResponse(this IQueryable<EmploymentAssignment> query)
    {
        return query
            .Where(x => x.Type == AssignmentType.Position)
            .Select(x => new CollaboratorPositionResponse
            {
                CollaboratorId = x.CollaboratorId,
                PositionId = x.ReferenceId,
                PositionName = "",
                Status = x.Status.ToString(),
                CreatedAt = x.CreatedAt
            });
    }
}