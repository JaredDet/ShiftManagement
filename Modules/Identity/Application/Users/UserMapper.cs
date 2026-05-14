using ShiftManagement.Api.Modules.Identity.Api.Contracts.Users;
using ShiftManagement.Api.Modules.Identity.Domain;

namespace ShiftManagement.Api.Modules.Identity.Application.Users;

public static class UserMapper
{
    public static UserResponse ToResponse(User user)
    {
        return new UserResponse(
            user.Id,
            user.CompanyId,
            user.Name,
            user.Email,
            user.Status.ToString().ToLowerInvariant(),
            user.CreatedAt
        );
    }
}