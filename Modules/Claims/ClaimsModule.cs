using ShiftManagement.Api.Modules.Claims.Application.Submissions;
using ShiftManagement.Api.Modules.Claims.Application.Reviews;
using ShiftManagement.Api.Modules.Claims.Infrastructure;
using ShiftManagement.Api.Modules.Claims.Application.Retrievalss;

namespace ShiftManagement.Api.Modules.Claims;

public static class ClaimsModule
{
    public static IServiceCollection AddClaims(
        this IServiceCollection services
    )
    {
        services.AddScoped<ClaimRepository>();
        services.AddScoped<ClaimReadRepositoryPostgres>();

        services.AddScoped<GetClaimUseCase>();
        services.AddScoped<ListClaimsUseCase>();

        services.AddScoped<AssignClaimUseCase>();
        services.AddScoped<CancelClaimUseCase>();
        services.AddScoped<RejectClaimUseCase>();
        services.AddScoped<ReopenClaimUseCase>();
        services.AddScoped<ResolveClaimUseCase>();
        services.AddScoped<StartClaimReviewUseCase>();

        services.AddScoped<AttachEvidenceToClaimUseCase>();
        services.AddScoped<CreateClaimUseCase>();
        services.AddScoped<UpdateClaimUseCase>();

        return services;
    }
}