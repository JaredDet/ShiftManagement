using ShiftManagement.Api.BuildingBlocks.Exceptions;

namespace ShiftManagement.Api.Modules.Staff.Domain;

public static class StaffExceptions
{
    public static DomainException InvalidAssignmentReference()
    {
        return new DomainException(
            "staff.assignment.invalid_reference",
            "Assignment reference is invalid",
            ErrorType.Validation
        );
    }

    public static DomainException AssignmentAlreadyExists(
        Guid employeeId,
        Guid referenceId,
        AssignmentType type
    )
    {
        return new DomainException(
            "staff.assignment.already_exists",
            $"Assignment '{type}' with reference '{referenceId}' already exists for employee '{employeeId}'",
            ErrorType.Conflict
        );
    }

    public static DomainException AssignmentNotFound(
        Guid referenceId,
        AssignmentType type
    )
    {
        return new DomainException(
            "staff.assignment.not_found",
            $"Assignment '{type}' with reference '{referenceId}' was not found",
            ErrorType.NotFound
        );
    }

    public static DomainException PrimaryAssignmentNotFound(
        Guid referenceId,
        AssignmentType type
    )
    {
        return new DomainException(
            "staff.assignment.primary_not_found",
            $"Cannot set primary assignment '{type}' with reference '{referenceId}' because it does not exist",
            ErrorType.NotFound
        );
    }
}