using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Identity.Domain;
using ShiftManagement.Api.Modules.Organization.Domain;
using ShiftManagement.Api.Modules.Session.Api.Contracts;
using ShiftManagement.Api.Modules.Staff.Domain;

namespace ShiftManagement.Api.Modules.Session.Infrastructure;

public static class SessionProjections
{
    public static IQueryable<SessionResponse> ToSessionContext(
        this DbContext context,
        Guid userId,
        Guid companyId
    )
    {
        var collaborators = context.Set<Collaborator>().AsNoTracking();
        var companies = context.Set<Company>().AsNoTracking();
        var users = context.Set<User>().AsNoTracking();
        var userRoles = context.Set<UserRole>().AsNoTracking();
        var branches = context.Set<Branch>().AsNoTracking();
        var assignments = context.Set<EmploymentAssignment>().AsNoTracking();

        var query =
            from c in collaborators
            join co in companies on c.CompanyId equals co.Id
            join u in users on c.UserId equals u.Id

            where c.UserId == userId
               && c.CompanyId == companyId

            select new SessionResponse
            {
                UserId = u.Id,
                Name = u.Name,
                Email = u.Email,

                Company = new CompanyResponse
                {
                    Id = co.Id,
                    Name = co.Name
                },

                MainBranch = branches
                    .Where(b =>
                        b.Id ==
                        assignments
                            .Where(a =>
                                a.CollaboratorId == c.Id &&
                                a.Type == AssignmentType.Branch &&
                                a.IsPrimary &&
                                a.Status == AssignmentStatus.Active
                            )
                            .Select(a => a.ReferenceId)
                            .FirstOrDefault()
                    )
                    .Select(b => new MainBranchResponse
                    {
                        Id = b.Id,
                        Name = b.Name
                    })
                    .FirstOrDefault()!,

                Role = userRoles
                    .Where(ur =>
                        ur.UserId == userId &&
                        ur.Status == UserRoleStatus.Active
                    )
                    .Select(ur => ur.Role.ToString())
                    .FirstOrDefault() ?? string.Empty
            };

        return query;
    }
}