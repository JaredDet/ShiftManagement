using ShiftManagement.Api.Modules.Contracts.Application.Commands;
using ShiftManagement.Api.Modules.Contracts.Application.Queries;
using ShiftManagement.Api.Modules.Contracts.Infrastructure;
using ShiftManagement.Api.Modules.Contracts.Infrastructure.Pdf;

namespace ShiftManagement.Api.Modules.Contracts;

public static class ContractsModule
{
    public static IServiceCollection AddContracts(
        this IServiceCollection services
    )
    {
        services.AddScoped<ContractReadRepository>();
        services.AddScoped<ContractRepository>();

        services.AddScoped<ContractPdfBuilder>();

        services.AddScoped<CreateContractUseCase>();
        services.AddScoped<TerminateContractUseCase>();

        services.AddScoped<ListCollaboratorContractsUseCase>();
        services.AddScoped<DownloadContractPdfUseCase>();
        services.AddScoped<GetActiveContractUseCase>();
        services.AddScoped<GetContractUseCase>();
        services.AddScoped<GetMyContractUseCase>();

        return services;
    }
}