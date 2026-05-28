using ShiftManagement.Api.BuildingBlocks.Exceptions;

namespace ShiftManagement.Api.Modules.Staff.Application.Errors;

public static class StaffErrors
{
    // ------------------------
    // COLLABORATOR
    // ------------------------

    public static readonly Error CollaboratorNotFound =
        new("staff.collaborator.not_found", "Collaborator not found.", ErrorType.NotFound);

    public static readonly Error CollaboratorAlreadyExists =
        new("staff.collaborator.already_exists", "Collaborator already exists.", ErrorType.Conflict);

    public static readonly Error CollaboratorInactive =
        new("staff.collaborator.inactive", "Collaborator is inactive.", ErrorType.Forbidden);

    // ------------------------
    // POSITION
    // ------------------------

    public static readonly Error PositionNotFound =
        new("staff.position.not_found", "Position not found.", ErrorType.NotFound);

    public static readonly Error PositionAlreadyExists =
            new(
                "staff.position.already_exists",
                "Position already exists in this company.", ErrorType.Conflict
            );

    public static readonly Error PositionInactive =
        new("staff.position.inactive", "Position is inactive.", ErrorType.Validation);

    // ------------------------
    // ASSIGNMENTS (GENERIC)
    // ------------------------

    public static readonly Error AssignmentNotFound =
        new("staff.assignment.not_found", "Assignment not found.", ErrorType.NotFound);

    public static readonly Error AssignmentAlreadyExists =
        new("staff.assignment.already_exists", "Assignment already exists.", ErrorType.Conflict);

    public static readonly Error AssignmentInactive =
        new("staff.assignment.inactive", "Assignment is not active.", ErrorType.Validation);

    public static readonly Error InvalidAssignmentType =
        new("staff.assignment.invalid_type", "Invalid assignment type.", ErrorType.Validation);

    // ------------------------
    // BUSINESS RULES (PRIMARY LOGIC)
    // ------------------------

    public static readonly Error InvalidPrimaryBranch =
        new(
            "staff.collaborator.invalid_primary_branch",
            "Collaborator does not have this branch assigned.",
            ErrorType.Validation
        );

    public static readonly Error InvalidPrimaryPosition =
        new(
            "staff.collaborator.invalid_primary_position",
            "Collaborator does not have this position assigned.",
            ErrorType.Validation
        );

    public static readonly Error InvalidPrimaryAssignmentState =
        new(
            "staff.collaborator.invalid_primary_state",
            "Collaborator primary assignment state is invalid.",
            ErrorType.Validation
        );

    public static readonly Error UserNotEligibleForCollaborator =
        new(
            "staff.collaborator.user_not_eligible",
            "User does not have a valid role to become an collaborator.",
            ErrorType.Validation
        );
}