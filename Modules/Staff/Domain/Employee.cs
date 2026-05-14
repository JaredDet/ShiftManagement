using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Staff.Domain;

[Table("employee")]
public class Employee
{
    [Key]
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }
    public Guid CompanyId { get; private set; }
    public EmployeeStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private readonly List<EmploymentAssignment> _assignments = new();

    public IReadOnlyCollection<EmploymentAssignment> Assignments => _assignments;

    private Employee() { }

    private Employee(Guid id, Guid userId, Guid companyId, DateTime createdAt)
    {
        Id = id;
        UserId = userId;
        CompanyId = companyId;
        CreatedAt = createdAt;
        Status = EmployeeStatus.Active;
    }

    public static Employee Create(Guid userId, Guid companyId)
        => new(Guid.NewGuid(), userId, companyId, DateTime.UtcNow);

    public void Activate() => Status = EmployeeStatus.Active;
    public void Deactivate() => Status = EmployeeStatus.Inactive;

    public Result AddAssignment(Guid referenceId, AssignmentType type, bool isPrimary)
    {
        if (referenceId == Guid.Empty)
            return Result.Failure(StaffErrors.InvalidAssignmentType);

        if (_assignments.Any(x =>
            x.ReferenceId == referenceId &&
            x.Type == type &&
            x.Status == AssignmentStatus.Active))
        {
            return Result.Failure(StaffErrors.AssignmentAlreadyExists);
        }

        var assignment = EmploymentAssignment.Create(
            Id,
            referenceId,
            type,
            isPrimary,
            DateTime.UtcNow
        );

        _assignments.Add(assignment);

        return Result.Success();
    }

    public Result RemoveAssignment(Guid referenceId, AssignmentType type)
    {
        var assignment = _assignments.FirstOrDefault(x =>
            x.ReferenceId == referenceId &&
            x.Type == type &&
            x.Status == AssignmentStatus.Active);

        if (assignment is null)
            return Result.Failure(StaffErrors.AssignmentNotFound);

        assignment.Deactivate();

        return Result.Success();
    }

    public Result SetPrimary(Guid referenceId, AssignmentType type)
    {
        var active = _assignments
            .Where(x => x.Type == type && x.Status == AssignmentStatus.Active)
            .ToList();

        var target = active.FirstOrDefault(x =>
            x.ReferenceId == referenceId);

        if (target is null)
            return Result.Failure(StaffErrors.AssignmentNotFound);

        foreach (var assignment in active)
            assignment.UnmarkAsPrimary();

        target.MarkAsPrimary();

        return Result.Success();
    }
}