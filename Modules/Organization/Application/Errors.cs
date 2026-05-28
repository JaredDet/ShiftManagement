using ShiftManagement.Api.BuildingBlocks.Exceptions;

namespace ShiftManagement.Api.Modules.Organization.Application.Errors;

public static class OrganizationErrors
{
    public static readonly Error CompanyNotFound =
        new(
            "organization.company.not_found",
            "Company not found.",
            ErrorType.NotFound
        );

    public static readonly Error CompanyAlreadyExists =
        new(
            "organization.company.already_exists",
            "Company already exists.",
            ErrorType.Conflict
        );

    public static readonly Error BranchNotFound =
        new(
            "organization.branch.not_found",
            "Branch not found.",
            ErrorType.NotFound
        );

    public static readonly Error BranchAlreadyExists =
    new(
        "organization.branch.already_exists",
        "Branch already exists for this company.",
        ErrorType.Conflict
    );
}