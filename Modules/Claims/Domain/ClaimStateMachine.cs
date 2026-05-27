using ShiftManagement.Api.Modules.Claims.Application;

namespace ShiftManagement.Api.Modules.Claims.Domain;

public static class ClaimStateMachine
{
    private static readonly Dictionary<
        (ClaimStatus State, ClaimAction Action),
        ClaimStatus
    > Transitions = new()
    {
        [(ClaimStatus.Pending, ClaimAction.StartReview)] = ClaimStatus.InReview,
        [(ClaimStatus.Pending, ClaimAction.Cancel)] = ClaimStatus.Canceled,

        [(ClaimStatus.InReview, ClaimAction.Resolve)] = ClaimStatus.Resolved,
        [(ClaimStatus.InReview, ClaimAction.Reject)] = ClaimStatus.Rejected,

        [(ClaimStatus.Rejected, ClaimAction.Reopen)] = ClaimStatus.Pending
    };

    public static ClaimStatus Transition(
        ClaimStatus current,
        ClaimAction action)
    {
        if (!Transitions.TryGetValue((current, action), out var next))
            throw ClaimStateErrors.InvalidStateTransition(current, action);

        return next;
    }

    public static bool IsTerminal(ClaimStatus status)
    {
        return status is
            ClaimStatus.Resolved or
            ClaimStatus.Rejected or
            ClaimStatus.Canceled;
    }
}