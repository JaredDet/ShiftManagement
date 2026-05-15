using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Identity.Domain;
using ShiftManagement.Api.Modules.Organization.Domain;
using ShiftManagement.Api.Modules.Scheduling.Domain;
using ShiftManagement.Api.Modules.Staff.Domain;

namespace ShiftManagement.Api.Infrastructure;

public sealed class ShiftManagementDbContext : DbContext
{
    public DbSet<Company> Companies => Set<Company>();

    public DbSet<Branch> Branches => Set<Branch>();


    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Position> Positions => Set<Position>();

    public DbSet<EmploymentAssignment> EmploymentAssignments
        => Set<EmploymentAssignment>();

    public DbSet<User> Users => Set<User>();
    public DbSet<UserCredential> UserCredentials => Set<UserCredential>();

    public DbSet<UserRole> UserRoles => Set<UserRole>();

    public DbSet<Shift> Shifts => Set<Shift>();
    public DbSet<ShiftAssignment> ShiftAssignments => Set<ShiftAssignment>();
    public DbSet<ShiftSwapRequest> ShiftSwapRequests => Set<ShiftSwapRequest>();

    public ShiftManagementDbContext(
        DbContextOptions<ShiftManagementDbContext> options
    ) : base(options)
    {
    }
}