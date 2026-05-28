using ShiftManagement.Api.Modules.Identity.Domain;
using ShiftManagement.Api.Modules.Identity.Infrastructure;
using ShiftManagement.Api.Modules.Identity.Api.Contracts.RoleAssignments;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Identity.Application.RoleAssignments;

public sealed class AssignRoleToUserUseCase(
    UserRepository userRepository,
    UserRoleRepository userRoleRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<UserRoleResponse>> ExecuteAsync(
        Guid userId,
        AssignRoleToUserRequest request
    )
    {
        var user = await userRepository.GetByIdAsync(userId);

        if (user is null)
            return Result<UserRoleResponse>.Failure(IdentityErrors.UserNotFound);

        if (RoleRules.RequiresBranch(request.Role) && request.BranchId is null)
            return Result<UserRoleResponse>.Failure(IdentityErrors.BranchRequiredForRole);

        if (RoleRules.DisallowsBranch(request.Role) && request.BranchId is not null)
            return Result<UserRoleResponse>.Failure(IdentityErrors.BranchNotAllowedForRole);

        var alreadyExists = await userRoleRepository.ExistsAsync(
            userId,
            request.Role,
            request.BranchId
        );

        if (alreadyExists)
            return Result<UserRoleResponse>.Failure(IdentityErrors.RoleAlreadyAssigned);

        var userRole = UserRole.Create(
            userId,
            request.Role,
            request.BranchId
        );

        await userRoleRepository.AddAsync(userRole);
        await context.SaveChangesAsync();

        return Result<UserRoleResponse>.Success(
            UserRoleMapper.ToResponse(userRole)
        );
    }
}