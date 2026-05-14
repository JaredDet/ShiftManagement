using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Identity.Infrastructure;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Identity.Application.Users;

public sealed class DeactivateUserUseCase(
    UserRepository userRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result> ExecuteAsync(Guid userId)
    {
        var user = await userRepository.GetByIdAsync(userId);

        if (user is null)
            return Result.Failure(IdentityErrors.UserNotFound);

        user.Deactivate();

        await context.SaveChangesAsync();

        return Result.Success();
    }
}