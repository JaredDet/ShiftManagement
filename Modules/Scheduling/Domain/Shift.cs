using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftManagement.Api.Modules.Scheduling.Domain;

[Table("shifts")]
public sealed class Shift
{
    [Key]
    public Guid Id { get; private set; }

    public Guid BranchId { get; private set; }

    public Guid PositionId { get; private set; }

    public DateTime StartsAt { get; private set; }

    public DateTime EndsAt { get; private set; }

    public ShiftStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private Shift()
    {
    }

    public Shift(
        Guid id,
        Guid branchId,
        Guid positionId,
        DateTime startsAt,
        DateTime endsAt,
        ShiftStatus status,
        DateTime createdAt)
    {
        Id = id;
        BranchId = branchId;
        PositionId = positionId;
        StartsAt = startsAt;
        EndsAt = endsAt;
        Status = status;
        CreatedAt = createdAt;
    }

    public static Shift Create(
        Guid branchId,
        Guid positionId,
        DateTime startsAt,
        DateTime endsAt)
    {
        return new Shift(
            Guid.NewGuid(),
            branchId,
            positionId,
            startsAt,
            endsAt,
            ShiftStatus.Active,
            DateTime.UtcNow
        );
    }

    public void Cancel()
    {
        Status = ShiftStatus.Cancelled;
    }

    public void Reschedule(DateTime startsAt, DateTime endsAt)
    {
        StartsAt = startsAt;
        EndsAt = endsAt;
    }

    public void Update(
    Guid branchId,
    Guid positionId,
    DateTime startsAt,
    DateTime endsAt)
    {
        BranchId = branchId;
        PositionId = positionId;
        StartsAt = startsAt;
        EndsAt = endsAt;
    }

    public void EnsureCanParticipateInSwap()
    {
        if (Status != ShiftStatus.Active)
            throw ShiftErrors.InvalidStateForSwap();
    }
}