using ShiftManagement.Api.Modules.Identity.Domain;

namespace ShiftManagement.Api.Modules.Identity.Infrastructure;

public sealed class StaffAccessPolicy(UserRoleRepository userRoleRepository)
{
    private readonly UserRoleRepository _userRoleRepository = userRoleRepository;

    public Task<bool> CanAccess(Guid userId)
    {
        return _userRoleRepository.UserHasRoleAsync(userId, Role.Staff);
    }
}