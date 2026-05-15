using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Identity.Api.Contracts.Users;
using ShiftManagement.Api.Modules.Identity.Domain;
using ShiftManagement.Api.Modules.Identity.Infrastructure;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Identity.Application.Users;

public sealed class CreateUserUseCase(
    UserRepository userRepository,
    UserCredentialRepository userCredentialRepository,
    CompanyRepository companyRepository,
    Argon2PasswordHasher argon2PasswordHasher,
    ShiftManagementDbContext context
)
{
    public async Task<Result<UserResponse>> ExecuteAsync(CreateUserRequest request)
    {
        var companyExists = await companyRepository.ExistsAsync(request.CompanyId);

        if (!companyExists)
            return Result<UserResponse>.Failure(OrganizationErrors.CompanyNotFound);

        var emailNormalized = request.Email.Trim().ToLowerInvariant();

        var existingUser = await userRepository.GetByEmailAsync(emailNormalized);

        if (existingUser is not null)
            return Result<UserResponse>.Failure(IdentityErrors.EmailAlreadyInUse);

        var user = new User(
            request.CompanyId,
            request.Name,
            emailNormalized
        );

        var passwordHash = argon2PasswordHasher
            .Hash(request.Password);

        var credential = new UserCredential(
            user.Id,
            passwordHash
        );

        await userRepository.AddAsync(user);
        await userCredentialRepository.AddAsync(credential);

        await context.SaveChangesAsync();

        return Result<UserResponse>.Success(
            UserMapper.ToResponse(user)
        );
    }
}