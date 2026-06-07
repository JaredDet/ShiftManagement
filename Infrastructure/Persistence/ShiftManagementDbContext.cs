using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShiftManagement.Api.Modules.Claims.Domain;
using ShiftManagement.Api.Modules.Contracts.Domain;
using ShiftManagement.Api.Modules.Identity.Domain;
using ShiftManagement.Api.Modules.Organization.Domain;
using ShiftManagement.Api.Modules.Scheduling.Domain;
using ShiftManagement.Api.Modules.Staff.Domain;

namespace ShiftManagement.Api.Infrastructure.Persistence;

public sealed class ShiftManagementDbContext(
    DbContextOptions<ShiftManagementDbContext> options
) : DbContext(options)
{
    public DbSet<Company> Companies => Set<Company>();

    public DbSet<EmploymentContract> EmploymentContracts => Set<EmploymentContract>();
    public DbSet<ContractTermination> ContractTerminations => Set<ContractTermination>();

    public DbSet<Branch> Branches => Set<Branch>();

    public DbSet<Collaborator> Collaborators => Set<Collaborator>();

    public DbSet<Position> Positions => Set<Position>();
    public DbSet<Claim> Claims => Set<Claim>();
    public DbSet<ClaimComment> ClaimComments => Set<ClaimComment>();
    public DbSet<ClaimEvidence> ClaimEvidences => Set<ClaimEvidence>();

    public DbSet<EmploymentAssignment> EmploymentAssignments
        => Set<EmploymentAssignment>();

    public DbSet<User> Users => Set<User>();

    public DbSet<UserCredential> UserCredentials
        => Set<UserCredential>();

    public DbSet<UserRole> UserRoles => Set<UserRole>();

    public DbSet<Shift> Shifts => Set<Shift>();

    public DbSet<ShiftAssignment> ShiftAssignments
        => Set<ShiftAssignment>();

    public DbSet<ShiftSwapRequest> ShiftSwapRequests
        => Set<ShiftSwapRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var utcConverter = new ValueConverter<DateTime, DateTime>(
            toDb => NormalizeUtc(toDb),

            fromDb => DateTime.SpecifyKind(
                fromDb,
                DateTimeKind.Utc
            )
        );

        var nullableUtcConverter =
            new ValueConverter<DateTime?, DateTime?>(
                toDb => toDb.HasValue
                    ? NormalizeUtc(toDb.Value)
                    : toDb,

                fromDb => fromDb.HasValue
                    ? DateTime.SpecifyKind(
                        fromDb.Value,
                        DateTimeKind.Utc
                    )
                    : fromDb
            );

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(utcConverter);
                }

                if (property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(nullableUtcConverter);
                }
            }
        }
    }

    private static DateTime NormalizeUtc(DateTime dt)
    {
        return dt.Kind switch
        {
            DateTimeKind.Utc => dt,

            DateTimeKind.Local =>
                dt.ToUniversalTime(),

            DateTimeKind.Unspecified =>
                DateTime.SpecifyKind(
                    dt,
                    DateTimeKind.Utc
                ),

            _ => dt
        };
    }
}