using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Identity.Api.Contracts.Users;
using ShiftManagement.Api.Modules.Identity.Infrastructure;

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

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        var existingUser = await userRepository.GetByEmailAsync(normalizedEmail);

        if (existingUser is not null && existingUser.Id != userId)
            return Result<UserResponse>.Failure(
                IdentityErrors.EmailAlreadyInUse
            );

        user.Update(
            request.Name,
            normalizedEmail
        );

        await context.SaveChangesAsync();

        return Result<UserResponse>.Success(
            UserMapper.ToResponse(user)
        );
    }
}