using ShiftManagement.Api.Modules.Staff.Api.Contracts.BranchAssignments;
using ShiftManagement.Api.Modules.Staff.Domain;

namespace ShiftManagement.Api.Modules.Staff.Application.Projections;

public static class BranchAssignmentProjection
{
    public static IQueryable<CollaboratorBranchResponse> ToBranchResponse(this IQueryable<EmploymentAssignment> query)
    {
        return query
            .Where(x => x.Type == AssignmentType.Branch)
            .Select(x => new CollaboratorBranchResponse
            {
                CollaboratorId = x.EmployeeId,
                BranchId = x.ReferenceId,
                BranchName = "",
                Status = x.Status.ToString(),
                CreatedAt = x.CreatedAt
            });
    }
}