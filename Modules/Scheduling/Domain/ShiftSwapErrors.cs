using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Scheduling.Domain;

public static class ShiftSwapErrors
{
    public static DomainException InvalidStateTransition(
    ShiftSwapStatus current,
    ShiftSwapAction action)
    {
        return new DomainException(
            "scheduling.shift_swap.invalid_transition",

            $"Cannot execute action '{action}' " +
            $"when shift swap is in state '{current}'"
        );
    }
}