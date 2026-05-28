using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Identity.Api.Contracts.RoleAssignments;
using ShiftManagement.Api.Modules.Identity.Infrastructure;

namespace ShiftManagement.Api.Modules.Identity.Application.RoleAssignments;

public sealed class ListUserRolesUseCase(
    UserRoleRepository userRoleRepository
)
{
    public async Task<Result<List<UserRoleResponse>>> ExecuteAsync(Guid userId)
    {
        var roles = await userRoleRepository.GetByUserIdAsync(userId);

        return Result<List<UserRoleResponse>>.Success(
            [.. roles.Select(UserRoleMapper.ToResponse)]
        );
    }
}