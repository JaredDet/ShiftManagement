using System.Text;
using System.Text.Json.Serialization;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShiftManagement.Api.BuildingBlocks.Execution;
using ShiftManagement.Api.BuildingBlocks.Storage;
using ShiftManagement.Api.Infrastructure.Auth;
using ShiftManagement.Api.Infrastructure.Execution;
using ShiftManagement.Api.Infrastructure.Middleware;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Infrastructure.Storage;
using ShiftManagement.Api.Modules.Claims;
using ShiftManagement.Api.Modules.Dev;
using ShiftManagement.Api.Modules.Identity;
using ShiftManagement.Api.Modules.Organization;
using ShiftManagement.Api.Modules.Scheduling;
using ShiftManagement.Api.Modules.Staff;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

foreach (System.Collections.DictionaryEntry kv in Environment.GetEnvironmentVariables())
{
    builder.Configuration[kv.Key.ToString()!] = kv.Value?.ToString();
}

// (opcional debug)
Console.WriteLine("PWD: " + Directory.GetCurrentDirectory());
Console.WriteLine("APP ROOT: " + AppContext.BaseDirectory);

Console.WriteLine("JWT KEY: " + builder.Configuration["Jwt__Key"]);
Console.WriteLine("JWT ISSUER: " + builder.Configuration["Jwt__Issuer"]);
Console.WriteLine("JWT AUDIENCE: " + builder.Configuration["Jwt__Audience"]);

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
    var connectionString = builder.Configuration["DATABASE_URL"];
    options.UseNpgsql(connectionString);
});

// =========================
// AUTH
// =========================

var jwtKey = builder.Configuration["Jwt__Key"];
var jwtIssuer = builder.Configuration["Jwt__Issuer"];
var jwtAudience = builder.Configuration["Jwt__Audience"];

if (string.IsNullOrWhiteSpace(jwtKey))
    throw new InvalidOperationException("Jwt__Key is missing");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            )
        };
    });

builder.Services.AddAuthorization(options =>
{
    AuthorizationPolicies.AddPolicies(options);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IExecutionContext, HttpExecutionContext>();

builder.Services.AddOptions<StorageOptions>()
    .Bind(builder.Configuration.GetSection("Storage"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddScoped<IFileStorage, LocalFileStorage>();

// =========================
// MODULES
// =========================

builder.Services.AddOrganization();
builder.Services.AddStaff();
builder.Services.AddIdentityModule(builder.Configuration);
builder.Services.AddScheduling();
builder.Services.AddClaims();
builder.Services.AddODev();

var app = builder.Build();

// =========================
// PIPELINE
// =========================

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