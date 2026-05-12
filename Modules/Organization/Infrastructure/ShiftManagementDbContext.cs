using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Organization.Domain;

namespace ShiftManagement.Api.Modules.Organization.Infrastructure;

public sealed class ShiftManagementDbContext : DbContext
{
    public DbSet<Company> Companies => Set<Company>();

    public DbSet<Branch> Branches => Set<Branch>();

    public ShiftManagementDbContext(
        DbContextOptions<ShiftManagementDbContext> options
    ) : base(options)
    {
    }
}