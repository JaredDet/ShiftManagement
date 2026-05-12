using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Organization.Application.UseCases;
using ShiftManagement.Api.Modules.Organization.Infrastructure;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShiftManagementDbContext>(options =>
{
    var connectionString = Environment.GetEnvironmentVariable(
        "DATABASE_URL"
    );

    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<CompanyRepository>();

builder.Services.AddScoped<BranchRepository>();

builder.Services.AddScoped<CreateCompanyUseCase>();
builder.Services.AddScoped<GetCompanyUseCase>();
builder.Services.AddScoped<UpdateCompanyUseCase>();
builder.Services.AddScoped<DeactivateCompanyUseCase>();

builder.Services.AddScoped<CreateBranchUseCase>();
builder.Services.AddScoped<GetBranchUseCase>();
builder.Services.AddScoped<UpdateBranchUseCase>();
builder.Services.AddScoped<DeactivateBranchUseCase>();
builder.Services.AddScoped<ListBranchesUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();