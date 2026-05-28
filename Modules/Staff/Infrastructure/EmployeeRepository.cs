using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Infrastructure.Persistence;

namespace ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;

public class EmployeeRepository(ShiftManagementDbContext context)
{

    public Task<Employee?> GetByUserAndCompanyAsync(Guid userId, Guid companyId)
    {
        return context.Set<Employee>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.CompanyId == companyId
            );
    }

    public async Task AddAsync(Employee employee)
    {
        await context.Set<Employee>().AddAsync(employee);
    }

    public void Update(Employee employee)
    {
        context.Set<Employee>().Update(employee);
    }

    public void Remove(Employee employee)
    {
        context.Set<Employee>().Remove(employee);
    }

    public Task<Employee?> GetByIdAsync(Guid id)
    {
        return context.Set<Employee>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return context.Employees
            .AnyAsync(c => c.Id == id);
    }
}