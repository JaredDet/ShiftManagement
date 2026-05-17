using ShiftManagement.Api.Shared;
using ShiftManagement.Api.Modules.Identity.Domain;
using ShiftManagement.Api.Modules.Identity.Infrastructure;
using ShiftManagement.Api.Modules.Identity.Api.Contracts.Auth;
using ShiftManagement.Api.Modules.Identity.Application.Users;

namespace ShiftManagement.Api.Modules.Identity.Application;

public sealed class LoginUseCase(
    UserRepository userRepository,
    UserCredentialRepository credentialRepository,
    UserRoleRepository userRoleRepository,
    Argon2PasswordHasher argon2PasswordHasher,
    JwtTokenService tokenService
)
{
    public async Task<Result<LoginResponse>> ExecuteAsync(
        LoginRequest request
    )
    {
        var email = request.Email
            .Trim()
            .ToLowerInvariant();

        var user = await userRepository
            .GetByEmailAsync(email);

        if (user is null)
        {
            return Result<LoginResponse>.Failure(
                IdentityErrors.InvalidCredentials
            );
        }

        if (user.Status == UserStatus.Inactive)
        {
            return Result<LoginResponse>.Failure(
                IdentityErrors.UserInactive
            );
        }

        var credential = await credentialRepository
            .GetByUserIdAsync(user.Id);

        if (credential is null)
        {
            return Result<LoginResponse>.Failure(
                IdentityErrors.InvalidCredentials
            );
        }

        var passwordValid = argon2PasswordHasher.Verify(
            request.Password,
            credential.PasswordHash
        );

        if (!passwordValid)
        {
            return Result<LoginResponse>.Failure(
                IdentityErrors.InvalidCredentials
            );
        }

        var roles = await userRoleRepository
            .GetActiveByUserIdAsync(user.Id);

        var token = tokenService.GenerateToken(
            user,
            roles
        );

        return Result<LoginResponse>.Success(
            new LoginResponse
            {
                AccessToken = token,
                User = UserMapper.ToResponse(user)
            }
        );
    }
}