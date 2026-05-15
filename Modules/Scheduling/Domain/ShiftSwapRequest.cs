using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftManagement.Api.Modules.Scheduling.Domain;

[Table("shift_swap_requests")]
public sealed class ShiftSwapRequest
{
    [Key]
    public Guid Id { get; private set; }

    public Guid RequesterId { get; private set; }
    public Guid TargetCollaboratorId { get; private set; }

    public Guid SourceShiftId { get; private set; }
    public Guid TargetShiftId { get; private set; }

    public ShiftSwapStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? RespondedAt { get; private set; }
    public DateTime? ApprovedAt { get; private set; }

    private ShiftSwapRequest()
    {
    }

    public ShiftSwapRequest(
        Guid id,
        Guid requesterId,
        Guid targetCollaboratorId,
        Guid sourceShiftId,
        Guid targetShiftId,
        ShiftSwapStatus status,
        DateTime createdAt,
        DateTime? respondedAt,
        DateTime? approvedAt)
    {
        Id = id;
        RequesterId = requesterId;
        TargetCollaboratorId = targetCollaboratorId;
        SourceShiftId = sourceShiftId;
        TargetShiftId = targetShiftId;
        Status = status;
        CreatedAt = createdAt;
        RespondedAt = respondedAt;
        ApprovedAt = approvedAt;
    }

    public static ShiftSwapRequest Create(
        Guid requesterId,
        Guid targetCollaboratorId,
        Guid sourceShiftId,
        Guid targetShiftId)
    {
        return new ShiftSwapRequest(
            Guid.NewGuid(),
            requesterId,
            targetCollaboratorId,
            sourceShiftId,
            targetShiftId,
            ShiftSwapStatus.Pending,
            DateTime.UtcNow,
            null,
            null
        );
    }

    public void Accept()
    {
        Status = ShiftSwapStatus.AcceptedByCollaborator;
        RespondedAt = DateTime.UtcNow;
    }

    public void Reject()
    {
        Status = ShiftSwapStatus.Rejected;
        RespondedAt = DateTime.UtcNow;
    }

    public void Approve()
    {
        Status = ShiftSwapStatus.Approved;
        ApprovedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        Status = ShiftSwapStatus.Cancelled;
    }
}