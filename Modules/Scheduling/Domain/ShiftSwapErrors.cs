using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Scheduling.Domain;

public static class ShiftSwapErrors
{
    public static DomainException InvalidStateTransition(
        ShiftSwapStatus from,
        ShiftSwapStatus to)
    {
        return new DomainException(
            "scheduling.shift_swap.invalid_transition",

            $"Cannot transition shift swap " +
            $"from '{from}' to '{to}'"
        );
    }
}