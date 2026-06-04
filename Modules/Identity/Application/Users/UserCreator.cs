using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Identity.Domain;
using ShiftManagement.Api.Modules.Identity.Infrastructure;

namespace ShiftManagement.Api.Modules.Identity.Application.Users;

public sealed class UserCreator(
    UserRepository userRepository,
    UserCredentialRepository userCredentialRepository,
    Argon2PasswordHasher argon2PasswordHasher
)
{
    public async Task<Result<User>> CreateAsync(
        Guid companyId,
        string name,
        string email,
        string password
    )
    {
        var emailNormalized = email.Trim().ToLowerInvariant();

        var existingUser =
            await userRepository.GetByEmailAsync(emailNormalized);

        if (existingUser is not null)
            return Result<User>.Failure(
                IdentityErrors.EmailAlreadyInUse
            );

        var user = User.Create(
            companyId,
            name,
            emailNormalized
        );

        var credential = UserCredential.Create(
            user.Id,
            argon2PasswordHasher.Hash(password)
        );

        await userRepository.AddAsync(user);
        await userCredentialRepository.AddAsync(credential);

        return Result<User>.Success(user);
    }
}