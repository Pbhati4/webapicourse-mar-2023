using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.Domain;

public class DepartmentLookup : IDepartmentLookup
{
    private readonly EmployeeDataContext _context;
    private readonly MapperConfiguration _config;

    public DepartmentLookup(EmployeeDataContext context,MapperConfiguration config)
    {
        _context = context;
        _config = config;
    }
    public async Task<List<DepartmentItem>> GetDepartmentsAsync()
    {
        var response = await _context.Departments
            .OrderBy(dept =>dept.Code)
            .ProjectTo<DepartmentItem>(_config)
           // .Select(dept => new DepartmentItem(dept.Code, dept.Description))
            .ToListAsync();
        return response;
    }
}
