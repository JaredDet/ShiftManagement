using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Staff.Application.Errors;

public static class StaffErrors
{
    // ------------------------
    // EMPLOYEE
    // ------------------------

    public static readonly Error EmployeeNotFound =
        new("staff.employee.not_found", "Employee not found.");

    public static readonly Error EmployeeAlreadyExists =
        new("staff.employee.already_exists", "Employee already exists.");

    public static readonly Error EmployeeInactive =
        new("staff.employee.inactive", "Employee is inactive.");

    public static readonly Error EmployeeInvalidState =
        new("staff.employee.invalid_state", "Employee is in an invalid state.");

    // ------------------------
    // POSITION
    // ------------------------

    public static readonly Error PositionNotFound =
        new("staff.position.not_found", "Position not found.");

    public static readonly Error PositionInactive =
        new("staff.position.inactive", "Position is inactive.");

    // ------------------------
    // ASSIGNMENTS (GENERIC)
    // ------------------------

    public static readonly Error AssignmentNotFound =
        new("staff.assignment.not_found", "Assignment not found.");

    public static readonly Error AssignmentAlreadyExists =
        new("staff.assignment.already_exists", "Assignment already exists.");

    public static readonly Error AssignmentInactive =
        new("staff.assignment.inactive", "Assignment is not active.");

    public static readonly Error InvalidAssignmentType =
        new("staff.assignment.invalid_type", "Invalid assignment type.");

    // ------------------------
    // BUSINESS RULES (PRIMARY LOGIC)
    // ------------------------

    public static readonly Error InvalidPrimaryBranch =
        new(
            "staff.employee.invalid_primary_branch",
            "Employee does not have this branch assigned."
        );

    public static readonly Error InvalidPrimaryPosition =
        new(
            "staff.employee.invalid_primary_position",
            "Employee does not have this position assigned."
        );

    public static readonly Error InvalidPrimaryAssignmentState =
        new(
            "staff.employee.invalid_primary_state",
            "Employee primary assignment state is invalid."
        );

    public static readonly Error UserNotEligibleForEmployee =
        new(
            "staff.employee.user_not_eligible",
            "User does not have a valid role to become an employee."
        );
}