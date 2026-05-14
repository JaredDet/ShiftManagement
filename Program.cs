using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Identity.Application;
using ShiftManagement.Api.Modules.Identity.Repository;
using ShiftManagement.Api.Modules.Organization;
using ShiftManagement.Api.Modules.Staff;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("APP_PORT") ?? "5181";

builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShiftManagementDbContext>(options =>
{
    var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
    options.UseNpgsql(connectionString);
});

// --------------------
// Modules (clean entry point)
// --------------------
builder.Services.AddOrganization();
builder.Services.AddStaff();

builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserCredentialRepository>();

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