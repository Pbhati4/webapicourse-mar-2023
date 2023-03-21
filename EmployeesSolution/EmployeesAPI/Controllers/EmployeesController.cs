using EmployeesApi;

namespace EmployeesAPI.Controllers;

public class EmployeesController : ControllerBase
{
    private readonly ILookupEmployees _employeeLookupService;

    public EmployeesController(ILookupEmployees employeeLookupService)
    {
        _employeeLookupService = employeeLookupService;
    }

    [HttpGet("employees")]
    public async Task<ActionResult> GetAllEmployees([FromQuery]string dept= "ALL")
    {
        var response = new EmployeeSummaryResponse(18, 10, 8, dept);
        return Ok(response);
    }

    [HttpGet("/employees/{employeeId}")]
    public async Task<ActionResult<EmployeeResponse>> GetEmployeeById([FromRoute] string employeeId)
    {
        EmployeeResponse? response = await _employeeLookupService.GetEmployeeByIdAsync(employeeId);
        if (response is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(response);
        }
    }
}
