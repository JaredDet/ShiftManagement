using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Identity.Api.Contracts.RoleAssignments;
using ShiftManagement.Api.Modules.Identity.Infrastructure;

namespace ShiftManagement.Api.Modules.Identity.Application.RoleAssignments;

public sealed class RemoveRoleFromUserUseCase(
    UserRoleRepository userRoleRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result> ExecuteAsync(
        Guid userId,
        RemoveRoleFromUserRequest request
    )
    {
        var userRole = await userRoleRepository.GetAsync(
            userId,
            request.Role,
            request.BranchId
        );

        if (userRole is null)
            return Result.Failure(IdentityErrors.RoleNotFound);

        userRole.Deactivate();

        await context.SaveChangesAsync();

        return Result.Success();
    }
}