namespace ShiftManagement.Api.Modules.Scheduling.Domain;

public static class ShiftSwapStateMachine
{
    private static readonly HashSet<(ShiftSwapStatus From, ShiftSwapStatus To)>
        ValidTransitions =
        [
            (
                ShiftSwapStatus.Pending,
                ShiftSwapStatus.AcceptedByCollaborator
            ),

            (
                ShiftSwapStatus.Pending,
                ShiftSwapStatus.Rejected
            ),

            (
                ShiftSwapStatus.Pending,
                ShiftSwapStatus.Cancelled
            ),

            (
                ShiftSwapStatus.AcceptedByCollaborator,
                ShiftSwapStatus.Approved
            ),

            (
                ShiftSwapStatus.AcceptedByCollaborator,
                ShiftSwapStatus.Rejected
            )
        ];

    public static bool CanTransition(
        ShiftSwapStatus from,
        ShiftSwapStatus to)
    {
        return ValidTransitions.Contains((from, to));
    }

    public static void EnsureTransition(
        ShiftSwapStatus from,
        ShiftSwapStatus to)
    {
        if (!CanTransition(from, to))
        {
            throw ShiftSwapErrors.InvalidStateTransition(
                from,
                to
            );
        }
    }
}