using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Identity.Api.Contracts.Users;
using ShiftManagement.Api.Modules.Identity.Infrastructure;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Identity.Application.Users;

public sealed class UpdateUserUseCase(
    UserRepository userRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<UserResponse>> ExecuteAsync(
        Guid userId,
        UpdateUserRequest request
    )
    {
        var user = await userRepository.GetByIdAsync(userId);

        if (user is null)
            return Result<UserResponse>.Failure(IdentityErrors.UserNotFound);

        user.Update(
            request.Name,
            request.Email.Trim().ToLowerInvariant()
        );

        await context.SaveChangesAsync();

        return Result<UserResponse>.Success(
            UserMapper.ToResponse(user)
        );
    }
}