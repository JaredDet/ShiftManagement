
using ShiftManagement.Api.BuildingBlocks.Exceptions;

namespace ShiftManagement.Api.Modules.Scheduling.Domain;

public static class ShiftExceptions
{
    public static DomainException InvalidStateForSwap() =>
    new(
        "scheduling.shift.invalid_state_for_swap",
        "Shift cannot participate in a swap in its current state",
        ErrorType.Conflict);

    public static DomainException AssignmentCancelled() =>
    new(
        "scheduling.shift_assignment.cancelled",
        "Assignment is cancelled and cannot be modified",
        ErrorType.Conflict
    );
}