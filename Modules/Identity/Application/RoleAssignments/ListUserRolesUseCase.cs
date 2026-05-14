using ShiftManagement.Api.Modules.Identity.Api.Contracts.RoleAssignments;
using ShiftManagement.Api.Modules.Identity.Infrastructure;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Identity.Application.RoleAssignments;

public sealed class ListUserRolesUseCase(
    UserRoleRepository userRoleRepository
)
{
    public async Task<Result<UserRolesResponse>> ExecuteAsync(Guid userId)
    {
        var roles = await userRoleRepository.GetByUserIdAsync(userId);

        return Result<UserRolesResponse>.Success(
            new UserRolesResponse
            {
                Roles = roles.Select(UserRoleMapper.ToResponse).ToList()
            }
        );
    }
}