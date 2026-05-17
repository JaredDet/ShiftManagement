namespace ShiftManagement.Api.Modules.Scheduling.Domain;

public static class ShiftSwapStateMachine
{
    private static readonly Dictionary<
        (ShiftSwapStatus State, ShiftSwapAction Action),
        ShiftSwapStatus
    > Transitions =
        new()
        {
            [
                (
                    ShiftSwapStatus.Pending,
                    ShiftSwapAction.Accept
                )
            ] = ShiftSwapStatus.AcceptedByCollaborator,

            [
                (
                    ShiftSwapStatus.Pending,
                    ShiftSwapAction.Reject
                )
            ] = ShiftSwapStatus.Rejected,

            [
                (
                    ShiftSwapStatus.Pending,
                    ShiftSwapAction.Cancel
                )
            ] = ShiftSwapStatus.Cancelled,

            [
                (
                    ShiftSwapStatus.AcceptedByCollaborator,
                    ShiftSwapAction.Approve
                )
            ] = ShiftSwapStatus.Approved,

            [
                (
                    ShiftSwapStatus.AcceptedByCollaborator,
                    ShiftSwapAction.Reject
                )
            ] = ShiftSwapStatus.Rejected
        };

    public static ShiftSwapStatus Transition(
        ShiftSwapStatus current,
        ShiftSwapAction action)
    {
        if (
            !Transitions.TryGetValue(
                (current, action),
                out var nextState
            )
        )
        {
            throw ShiftSwapErrors.InvalidStateTransition(
                current,
                action
            );
        }

        return nextState;
    }
}