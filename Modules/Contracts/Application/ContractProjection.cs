using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Contracts.Api.Contracts.Queries;
using ShiftManagement.Api.Modules.Contracts.Api.Contracts.Query;
using ShiftManagement.Api.Modules.Contracts.Domain;
using ShiftManagement.Api.Modules.Contracts.Domain.Enums;
using ShiftManagement.Api.Modules.Identity.Domain;
using ShiftManagement.Api.Modules.Staff.Domain;

namespace ShiftManagement.Api.Modules.Contracts.Infrastructure;

public static class ContractProjection
{
    public static IQueryable<EmploymentContractResponse> ToContractResponse(
        this DbContext context,
        Guid? collaboratorId = null,
        Guid? contractId = null)
    {
        var contracts = context.Set<EmploymentContract>().AsNoTracking();
        var collaborators = context.Set<Collaborator>().AsNoTracking();
        var users = context.Set<User>().AsNoTracking();
        var terminations = context.Set<ContractTermination>().AsNoTracking();

        var query =
            from c in contracts

            join col in collaborators
                on c.CollaboratorId equals col.Id into colGroup
            from collaborator in colGroup.DefaultIfEmpty()

            join cu in users
                on collaborator.UserId equals cu.Id into userGroup
            from collaboratorUser in userGroup.DefaultIfEmpty()

            join creator in users
                on c.CreatedByUserId equals creator.Id into creatorGroup
            from createdByUser in creatorGroup.DefaultIfEmpty()

            join t in terminations
                on c.Id equals t.ContractId into terminationGroup
            from termination in terminationGroup.DefaultIfEmpty()

            join termUser in users
                on termination.TerminatedByUserId equals termUser.Id into termUserGroup
            from terminatedByUser in termUserGroup.DefaultIfEmpty()

            select new
            {
                c,
                collaboratorUser,
                createdByUser,
                termination,
                terminatedByUser
            };

        if (contractId.HasValue)
            query = query.Where(x => x.c.Id == contractId.Value);

        if (collaboratorId.HasValue)
            query = query.Where(x => x.c.CollaboratorId == collaboratorId.Value);

        return query.Select(x => new EmploymentContractResponse
        {
            Id = x.c.Id,

            CollaboratorId = x.c.CollaboratorId,
            CollaboratorName = x.collaboratorUser != null
                ? x.collaboratorUser.Name
                : "",

            ApprovedByUserId = x.c.CreatedByUserId,
            ApprovedByName = x.createdByUser != null
                ? x.createdByUser.Name
                : "",

            Type = x.c.Type.ToString(),
            Status = x.c.Status.ToString(),

            WorkScheduleType = x.c.WorkScheduleType.ToString(),

            SalaryAmount = x.c.SalaryAmount,

            Currency = x.c.Currency.ToString(),
            PayPeriod = x.c.PayPeriod.ToString(),

            StartsAt = x.c.StartDate,
            EndsAt = x.c.EndDate,

            CreatedAt = x.c.CreatedAt,

            TerminatedAt = x.termination != null
                ? x.termination.CreatedAt
                : null,

            Termination = x.termination != null
                ? new EmploymentContractTerminationResponse
                {
                    ApprovedByUserId = x.termination.TerminatedByUserId,
                    ApprovedByName = x.terminatedByUser != null
                        ? x.terminatedByUser.Name
                        : "",
                    Reason = x.termination.Reason.ToString(),
                    TerminationDate = x.termination.TerminationDate,
                    TerminatedAt = x.termination.CreatedAt,
                    Comment = x.termination.Comment
                }
                : null
        });
    }

    public static IQueryable<MyEmploymentContractResponse> ToMyContractResponse(
    this DbContext context,
    Guid userId)
    {
        var contracts = context.Set<EmploymentContract>().AsNoTracking();
        var collaborators = context.Set<Collaborator>().AsNoTracking();

        var query =
            from c in contracts
            join col in collaborators
                on c.CollaboratorId equals col.Id

            where col.UserId == userId
                  && c.Status == ContractStatus.ACTIVE

            select new MyEmploymentContractResponse
            {
                Id = c.Id,
                CollaboratorId = c.CollaboratorId,

                Type = c.Type.ToString(),
                WorkScheduleType = c.WorkScheduleType.ToString(),

                SalaryAmount = c.SalaryAmount,
                Currency = c.Currency.ToString(),
                PayPeriod = c.PayPeriod.ToString(),

                StartDate = c.StartDate,
                EndDate = c.EndDate
            };

        return query;
    }
}