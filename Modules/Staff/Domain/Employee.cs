using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public IReadOnlyCollection<EmploymentAssignment>
        Assignments => _assignments;

    private Employee() { }

    private Employee(
        Guid id,
        Guid userId,
        Guid companyId,
        DateTime createdAt
    )
    {
        Id = id;
        UserId = userId;
        CompanyId = companyId;
        CreatedAt = createdAt;
        Status = EmployeeStatus.Active;
    }

    public static Employee Create(
        Guid userId,
        Guid companyId
    )
    {
        return new Employee(
            Guid.NewGuid(),
            userId,
            companyId,
            DateTime.UtcNow
        );
    }

    public void Activate()
    {
        Status = EmployeeStatus.Active;
    }

    public void Deactivate()
    {
        Status = EmployeeStatus.Inactive;
    }

    public void AddAssignment(
        Guid referenceId,
        AssignmentType type,
        bool isPrimary
    )
    {
        if (referenceId == Guid.Empty)
        {
            throw StaffExceptions
                .InvalidAssignmentReference();
        }

        var alreadyExists = _assignments.Any(x =>
            x.ReferenceId == referenceId &&
            x.Type == type &&
            x.Status == AssignmentStatus.Active
        );

        if (alreadyExists)
        {
            throw StaffExceptions.AssignmentAlreadyExists(
                Id,
                referenceId,
                type
            );
        }

        var assignment = EmploymentAssignment.Create(
            Id,
            referenceId,
            type,
            isPrimary,
            DateTime.UtcNow
        );

        _assignments.Add(assignment);
    }

    public void RemoveAssignment(
        Guid referenceId,
        AssignmentType type
    )
    {
        var assignment = _assignments.FirstOrDefault(x =>
            x.ReferenceId == referenceId &&
            x.Type == type &&
            x.Status == AssignmentStatus.Active
        );

        if (assignment is null)
        {
            throw StaffExceptions.AssignmentNotFound(
                referenceId,
                type
            );
        }

        assignment.Deactivate();
    }

    public void SetPrimary(
        Guid referenceId,
        AssignmentType type
    )
    {
        var activeAssignments = _assignments
            .Where(x =>
                x.Type == type &&
                x.Status == AssignmentStatus.Active
            )
            .ToList();

        var target = activeAssignments.FirstOrDefault(x =>
            x.ReferenceId == referenceId
        );

        if (target is null)
        {
            throw StaffExceptions.PrimaryAssignmentNotFound(
                referenceId,
                type
            );
        }

        foreach (var assignment in activeAssignments)
        {
            assignment.UnmarkAsPrimary();
        }

        target.MarkAsPrimary();
    }
}