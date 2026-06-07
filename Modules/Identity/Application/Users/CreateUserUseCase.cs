using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Identity.Api.Contracts.Users;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

namespace ShiftManagement.Api.Modules.Identity.Application.Users;

public sealed class CreateUserUseCase(
    UserCreator userCreator,
    CompanyRepository companyRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<UserResponse>> ExecuteAsync(
        CreateUserRequest request
    )
    {
        var companyExists = await companyRepository.ExistsAsync(request.CompanyId);

        if (!companyExists)
            return Result<UserResponse>.Failure(
                OrganizationErrors.CompanyNotFound
            );

        var result = await userCreator.CreateAsync(
            request.CompanyId,
            request.Name,
            request.Email,
            request.Password
        );

        if (!result.IsSuccess)
            return Result<UserResponse>.Failure(result.Error!);

        await context.SaveChangesAsync();

        return Result<UserResponse>.Success(
            UserMapper.ToResponse(result.Value!)
        );
    }
}