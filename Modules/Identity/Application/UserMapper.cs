using ShiftManagement.Api.Modules.Identity.Api.Contracts;
using ShiftManagement.Api.Modules.Identity.Domain;

namespace ShiftManagement.Api.Modules.Identity.Application;

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