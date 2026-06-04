using ShiftManagement.Api.Modules.Organization.Application.Branches;
using ShiftManagement.Api.Modules.Organization.Application.Companies;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

namespace ShiftManagement.Api.Modules.Organization;

public static class OrganizationModule
{
    public static IServiceCollection AddOrganization(this IServiceCollection services)
    {
        services.AddScoped<CompanyRepository>();
        services.AddScoped<BranchRepository>();

        services.AddScoped<ListCompaniesUseCase>();
        services.AddScoped<GetCompanyUseCase>();

        services.AddScoped<CreateBranchUseCase>();
        services.AddScoped<GetBranchUseCase>();
        services.AddScoped<UpdateBranchUseCase>();
        services.AddScoped<DeactivateBranchUseCase>();
        services.AddScoped<ListBranchesUseCase>();

        return services;
    }
}