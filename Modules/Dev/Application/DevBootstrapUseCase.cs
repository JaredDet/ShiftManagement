using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Dev.Api;
using ShiftManagement.Api.Modules.Identity.Domain;
using ShiftManagement.Api.Modules.Organization.Domain;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Identity.Application.Users;
using ShiftManagement.Api.Modules.Identity.Application.RoleAssignments;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using Microsoft.EntityFrameworkCore;

namespace ShiftManagement.Api.Modules.Dev.Application;


public sealed class DevBootstrapUseCase(
    ShiftManagementDbContext db,
    UserCreator userCreator,
    RoleAssigner roleAssigner
)
{
    public async Task<Result<BootstrapResponse>> ExecuteAsync(
        BootstrapCompanyRequest request
    )
    {
        var companyExists =
            await db.Companies.AnyAsync(x =>
                x.Name == request.CompanyName);

        if (companyExists)
            return Result<BootstrapResponse>.Failure(
                OrganizationErrors.CompanyAlreadyExists);

        var company = Company.Create(
            request.CompanyName
        );

        db.Companies.Add(company);

        var userResult = await userCreator.CreateAsync(
            company.Id,
            request.AdminName,
            request.AdminEmail,
            request.Password
        );

        if (!userResult.IsSuccess)
            return Result<BootstrapResponse>.Failure(
                userResult.Error!
            );

        var roleResult = await roleAssigner.AssignAsync(
            userResult.Value!.Id,
            Role.CompanyAdmin,
            null
        );

        if (!roleResult.IsSuccess)
            return Result<BootstrapResponse>.Failure(
                roleResult.Error!
            );

        await db.SaveChangesAsync();

        return Result<BootstrapResponse>.Success(
            new BootstrapResponse(
                company.Id,
                userResult.Value.Id
            )
        );
    }
}