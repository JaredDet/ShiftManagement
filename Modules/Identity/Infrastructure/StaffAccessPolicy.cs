using ShiftManagement.Api.Modules.Identity.Domain;

namespace ShiftManagement.Api.Modules.Identity.Infrastructure;

public sealed class StaffAccessPolicy(UserRoleRepository userRoleRepository)
{

    public Task<bool> CanAccess(Guid userId)
    {
        return userRoleRepository.UserHasRoleAsync(userId, Role.Staff);
    }
}