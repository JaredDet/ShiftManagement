using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Organization.Application.Errors;

public static class OrganizationErrors
{
    public static readonly Error CompanyNotFound =
        new(
            "organization.company.not_found",
            "Company not found."
        );

    public static readonly Error BranchNotFound =
        new(
            "organization.branch.not_found",
            "Branch not found."
        );
}