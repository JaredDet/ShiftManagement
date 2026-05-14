using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Identity.Api.Contracts;
using ShiftManagement.Api.Modules.Identity.Domain;
using ShiftManagement.Api.Modules.Identity.Repository;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Identity.Application;

public sealed class CreateUserUseCase(
    UserRepository userRepository,
    UserCredentialRepository userCredentialRepository,
    CompanyRepository companyRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<UserResponse>> ExecuteAsync(
        CreateUserRequest request
    )
    {
        var companyExists = await companyRepository
            .ExistsAsync(request.CompanyId);

        if (!companyExists)
        {
            return Result<UserResponse>.Failure(
                OrganizationErrors.CompanyNotFound
            );
        }

        var existingUser = await userRepository
            .GetByEmailAsync(request.Email);

        if (existingUser is not null)
        {
            return Result<UserResponse>.Failure(
                IdentityErrors.EmailAlreadyInUse
            );
        }

        var user = new User(
            request.CompanyId,
            request.Name,
            request.Email.Trim().ToLowerInvariant()
        );

        var passwordHash = BCrypt.Net.BCrypt
            .HashPassword(request.Password);

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