using System.Text.Json.Serialization;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Identity;
using ShiftManagement.Api.Modules.Organization;
using ShiftManagement.Api.Modules.Scheduling;
using ShiftManagement.Api.Modules.Staff;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

var port = builder.Configuration["APP_PORT"] ?? "5181";

builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShiftManagementDbContext>(options =>
{
    var connectionString =
        builder.Configuration["DATABASE_URL"];

    options.UseNpgsql(connectionString);
});

builder.Services.AddAuthorization(options =>
{
    AuthorizationPolicies.AddPolicies(options);
});

builder.Services.AddOrganization();

builder.Services.AddStaff();

builder.Services.AddIdentityModule(builder.Configuration);

builder.Services.AddScheduling();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseMiddleware<DomainExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();