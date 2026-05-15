using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftManagement.Api.Modules.Scheduling.Domain;

[Table("shift_assignments")]
public sealed class ShiftAssignment
{
    [Key]
    public Guid Id { get; private set; }

    public Guid ShiftId { get; private set; }
    public Guid CollaboratorId { get; private set; }

    public ShiftAssignmentStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? CancelledAt { get; private set; }

    private ShiftAssignment()
    {
    }

    public ShiftAssignment(
        Guid id,
        Guid shiftId,
        Guid collaboratorId,
        ShiftAssignmentStatus status,
        DateTime createdAt,
        DateTime? cancelledAt)
    {
        Id = id;
        ShiftId = shiftId;
        CollaboratorId = collaboratorId;
        Status = status;
        CreatedAt = createdAt;
        CancelledAt = cancelledAt;
    }

    public static ShiftAssignment Create(Guid shiftId, Guid collaboratorId)
    {
        return new ShiftAssignment(
            Guid.NewGuid(),
            shiftId,
            collaboratorId,
            ShiftAssignmentStatus.Assigned,
            DateTime.UtcNow,
            null
        );
    }

    public void Cancel()
    {
        Status = ShiftAssignmentStatus.Cancelled;
        CancelledAt = DateTime.UtcNow;
    }
}