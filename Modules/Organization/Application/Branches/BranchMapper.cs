using ShiftManagement.Api.Modules.Organization.Api.Contracts;
using ShiftManagement.Api.Modules.Organization.Domain;

namespace ShiftManagement.Api.Modules.Organization.Application.Branches;

public static class BranchMapper
{
    public static BranchResponse ToResponse(Branch branch)
    {
        return new BranchResponse(
            branch.Id,
            branch.CompanyId,
            branch.Name,
            branch.Address,
            branch.Status.ToString().ToLowerInvariant(),
            branch.CreatedAt
        );
    }

    public static List<BranchResponse> ToResponseList(IEnumerable<Branch> branches)
        => [.. branches.Select(ToResponse)];
}