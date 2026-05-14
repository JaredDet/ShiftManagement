using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Identity.Domain;
using ShiftManagement.Api.Modules.Organization.Domain;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;
using ShiftManagement.Api.Modules.Staff.Domain;

namespace ShiftManagement.Api.Modules.Staff.Application.Projections;

public static class EmployeeProjections
{
    public static IQueryable<CollaboratorResponse> ToCollaboratorResponse(
    this DbContext context
)
    {
        var employees = context.Set<Employee>().AsNoTracking();
        var users = context.Set<User>().AsNoTracking();
        var branches = context.Set<Branch>().AsNoTracking();
        var positions = context.Set<Position>().AsNoTracking();

        var assignments = context.Set<EmploymentAssignment>()
            .AsNoTracking()
            .Where(x => x.Status == AssignmentStatus.Active);

        var query =
            from e in employees
            join u in users on e.UserId equals u.Id

            join a in assignments on e.Id equals a.EmployeeId into ag

            join ab in branches on
                ag.Where(x => x.Type == AssignmentType.Branch && x.IsPrimary)
                   .Select(x => x.ReferenceId)
                   .FirstOrDefault()
            equals ab.Id into branchJoin
            from b in branchJoin.DefaultIfEmpty()

            join ap in positions on
                ag.Where(x => x.Type == AssignmentType.Position && x.IsPrimary)
                   .Select(x => x.ReferenceId)
                   .FirstOrDefault()
            equals ap.Id into positionJoin
            from p in positionJoin.DefaultIfEmpty()

            select new CollaboratorResponse
            {
                Id = e.Id,
                UserId = e.UserId,
                CompanyId = e.CompanyId,

                Name = u.Name,
                Email = u.Email,

                MainBranchId = ag
                    .Where(x => x.Type == AssignmentType.Branch && x.IsPrimary)
                    .Select(x => x.ReferenceId)
                    .FirstOrDefault(),

                MainBranchName = b != null ? b.Name : null,

                MainPositionId = ag
                    .Where(x => x.Type == AssignmentType.Position && x.IsPrimary)
                    .Select(x => x.ReferenceId)
                    .FirstOrDefault(),

                MainPositionName = p != null ? p.Name : null,

                Status = e.Status.ToString(),
                CreatedAt = e.CreatedAt
            };

        return query;
    }
}