using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Infrastructure;

namespace ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;

public class EmployeeRepository
{
    private readonly ShiftManagementDbContext _context;

    public EmployeeRepository(ShiftManagementDbContext context)
    {
        _context = context;
    }

    public Task<Employee?> GetByUserAndCompanyAsync(Guid userId, Guid companyId)
    {
        return _context.Set<Employee>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.CompanyId == companyId
            );
    }

    public async Task AddAsync(Employee employee)
    {
        await _context.Set<Employee>().AddAsync(employee);
    }

    public void Update(Employee employee)
    {
        _context.Set<Employee>().Update(employee);
    }

    public void Remove(Employee employee)
    {
        _context.Set<Employee>().Remove(employee);
    }

    public Task<Employee?> GetByIdAsync(Guid id)
    {
        return _context.Set<Employee>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}