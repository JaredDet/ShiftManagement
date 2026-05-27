using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Scheduling.Application;

public static class SchedulingErrors
{
    public static readonly Error ShiftNotFound =
        new("scheduling.shift.not_found", "Shift not found", ErrorType.NotFound);

    public static readonly Error ShiftAlreadyExists =
        new("scheduling.shift.already_exists", "Shift already exists", ErrorType.Conflict);

    public static readonly Error ShiftInvalidTimeRange =
        new("scheduling.shift.invalid_time_range", "Shift end time must be after start time", ErrorType.Validation);

    public static readonly Error ShiftAlreadyAssigned =
        new("scheduling.shift.already_assigned", "Shift already has an active assignment", ErrorType.Conflict);

    public static readonly Error CollaboratorNotAvailable =
        new("scheduling.collaborator.not_available", "Collaborator is not available for this shift", ErrorType.Conflict);

    public static readonly Error InvalidShiftState =
        new("scheduling.shift.invalid_state", "Shift is in an invalid state for this operation", ErrorType.Conflict);

    public static readonly Error AssignmentNotFound =
        new("scheduling.assignment.not_found", "Assignment not found", ErrorType.NotFound);

    public static readonly Error SwapRequestNotFound =
        new("scheduling.swap.not_found", "Swap request not found", ErrorType.NotFound);

    public static readonly Error SwapAlreadyProcessed =
        new("scheduling.swap.already_processed", "Swap request already processed", ErrorType.Conflict);

    public static readonly Error InvalidSwapDecision =
        new("scheduling.swap.invalid_decision", "Invalid swap decision", ErrorType.Validation);
}