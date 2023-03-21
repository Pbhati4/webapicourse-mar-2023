using EmployeesApi.Models;

namespace EmployeesAPI.Domain
{
    public interface IDepartmentLookup
    {
        Task<List<DepartmentItem>> GetDepartmentsAsync();
    }
}