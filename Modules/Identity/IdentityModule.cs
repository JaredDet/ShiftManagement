using ShiftManagement.Api.Modules.Identity.Application;
using ShiftManagement.Api.Modules.Identity.Application.RoleAssignments;
using ShiftManagement.Api.Modules.Identity.Application.Users;
using ShiftManagement.Api.Modules.Identity.Infrastructure;

namespace ShiftManagement.Api.Modules.Identity;

public static class IdentityModule
{
    public static IServiceCollection AddIdentityModule(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<UserRepository>();
        services.AddScoped<UserRoleRepository>();
        services.AddScoped<UserCredentialRepository>();

        services.AddScoped<CreateUserUseCase>();
        services.AddScoped<UpdateUserUseCase>();
        services.AddScoped<DeactivateUserUseCase>();
        services.AddScoped<LoginUseCase>();

        services.AddScoped<AssignRoleToUserUseCase>();
        services.AddScoped<RemoveRoleFromUserUseCase>();
        services.AddScoped<ListUserRolesUseCase>();

        services.Configure<JwtOptions>(
            configuration.GetSection("Jwt")
        );

        services.AddScoped<StaffAccessPolicy>();

        services.AddScoped<JwtTokenService>();

        return services;
    }
}