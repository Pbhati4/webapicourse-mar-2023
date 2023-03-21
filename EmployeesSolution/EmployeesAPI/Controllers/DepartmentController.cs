using EmployeesApi.Models;

namespace EmployeesAPI.Controllers;

public class DepartmentController : ControllerBase
{
    private readonly DepartmentLookup _departmentLookup;

    public DepartmentController(DepartmentLookup departmentLookup)
    {
        _departmentLookup = departmentLookup;
    }

    [HttpGet("department")]
    public async Task<IActionResult> GetAllDepartments()
    {
        var data = await _departmentLookup.GetDepartmentsAsync();
        var response = new SharedCollectionResponse<DepartmentItem>() { Data = data };
        return Ok(response);
    }
}
