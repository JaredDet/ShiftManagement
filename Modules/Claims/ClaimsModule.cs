using ShiftManagement.Api.Modules.Claims.Application.Submissions;
using ShiftManagement.Api.Modules.Claims.Application.Reviews;
using ShiftManagement.Api.Modules.Claims.Infrastructure;
using ShiftManagement.Api.Modules.Claims.Application.Retrievals;

namespace ShiftManagement.Api.Modules.Claims;

public static class ClaimsModule
{
    public static IServiceCollection AddClaims(
        this IServiceCollection services
    )
    {
        services.AddScoped<ClaimRepository>();
        services.AddScoped<ClaimReadRepository>();

        services.AddScoped<GetClaimUseCase>();
        services.AddScoped<ListClaimsUseCase>();
        services.AddScoped<ListMyClaimsUseCase>();

        services.AddScoped<AssignClaimUseCase>();
        services.AddScoped<CancelClaimUseCase>();
        services.AddScoped<StartClaimReviewUseCase>();
        services.AddScoped<ResolveClaimUseCase>();
        services.AddScoped<RejectClaimUseCase>();
        services.AddScoped<ReopenClaimUseCase>();

        services.AddScoped<AttachEvidenceToClaimUseCase>();
        services.AddScoped<CreateClaimUseCase>();
        services.AddScoped<UpdateClaimUseCase>();

        return services;
    }
}