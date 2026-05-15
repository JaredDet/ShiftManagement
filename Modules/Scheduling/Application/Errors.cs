using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Scheduling.Application;

public static class SchedulingErrors
{
    public static readonly Error ShiftNotFound =
        new("SHIFT_NOT_FOUND", "Shift not found");

    public static readonly Error ShiftAlreadyExists =
        new("SHIFT_ALREADY_EXISTS", "Shift already exists");

    public static readonly Error ShiftInvalidTimeRange =
        new("SHIFT_INVALID_TIME_RANGE", "Shift end time must be after start time");

    public static readonly Error ShiftAlreadyAssigned =
        new("SHIFT_ALREADY_ASSIGNED", "Shift already has an active assignment");

    public static readonly Error CollaboratorNotAvailable =
        new("COLLABORATOR_NOT_AVAILABLE", "Collaborator is not available for this shift");

    public static readonly Error InvalidShiftState =
        new("INVALID_SHIFT_STATE", "Shift is in an invalid state for this operation");
}