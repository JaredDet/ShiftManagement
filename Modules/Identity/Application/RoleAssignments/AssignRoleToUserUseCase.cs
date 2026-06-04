using ShiftManagement.Api.Modules.Identity.Domain;
using ShiftManagement.Api.Modules.Identity.Infrastructure;
using ShiftManagement.Api.Modules.Identity.Api.Contracts.RoleAssignments;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Identity.Application.RoleAssignments;

public sealed class AssignRoleToUserUseCase(
    RoleAssigner roleAssigner,
    UserRepository userRepository,
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
            return Result<UserRoleResponse>.Failure(
                IdentityErrors.UserNotFound
            );

        var result = await roleAssigner.AssignAsync(
            userId,
            request.Role,
            request.BranchId
        );

        if (!result.IsSuccess)
            return Result<UserRoleResponse>.Failure(
                result.Error!
            );

        await context.SaveChangesAsync();

        return Result<UserRoleResponse>.Success(
            UserRoleMapper.ToResponse(result.Value!)
        );
    }
}