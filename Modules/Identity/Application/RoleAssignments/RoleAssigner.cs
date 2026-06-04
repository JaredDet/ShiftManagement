using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Identity.Domain;
using ShiftManagement.Api.Modules.Identity.Infrastructure;

namespace ShiftManagement.Api.Modules.Identity.Application.RoleAssignments;

public sealed class RoleAssigner(
    UserRoleRepository userRoleRepository
)
{
    public async Task<Result<UserRole>> AssignAsync(
        Guid userId,
        Role role,
        Guid? branchId
    )
    {
        if (RoleRules.RequiresBranch(role) && branchId is null)
            return Result<UserRole>.Failure(
                IdentityErrors.BranchRequiredForRole
            );

        if (RoleRules.DisallowsBranch(role) && branchId is not null)
            return Result<UserRole>.Failure(
                IdentityErrors.BranchNotAllowedForRole
            );

        var alreadyExists = await userRoleRepository.ExistsAsync(
            userId,
            role,
            branchId
        );

        if (alreadyExists)
            return Result<UserRole>.Failure(
                IdentityErrors.RoleAlreadyAssigned
            );

        var userRole = UserRole.Create(
            userId,
            role,
            branchId
        );

        await userRoleRepository.AddAsync(userRole);

        return Result<UserRole>.Success(userRole);
    }
}