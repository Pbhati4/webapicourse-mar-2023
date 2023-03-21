using EmployeesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.Domain;

public class DepartmentLookup : IDepartmentLookup
{
    private readonly EmployeeDataContext _context;
    public DepartmentLookup(EmployeeDataContext context)
    {
        _context = context;
    }
    public async Task<List<DepartmentItem>> GetDepartmentsAsync()
    {
        var response = await _context.Departments
            .Select(dept => new DepartmentItem(dept.Code, dept.Description))
            .ToListAsync();
        return response;
    }
}
