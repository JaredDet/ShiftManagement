using ShiftManagement.Api.BuildingBlocks.Exceptions;

namespace ShiftManagement.Api.Modules.Scheduling.Domain;

public static class ShiftSwapExceptions
{
    public static DomainException InvalidStateTransition(
    ShiftSwapStatus current,
    ShiftSwapAction action)
    {
        return new DomainException(
            "scheduling.shift_swap.invalid_transition",

            $"Cannot execute action '{action}' " +
            $"when shift swap is in state '{current}'",
            ErrorType.Validation
        );
    }
}