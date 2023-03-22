using EmployeesApi;
using EmployeesApi.Controllers.Domain;

namespace EmployeesAPI.Controllers;

public class EmployeesController : ControllerBase
{
    private readonly ILookupEmployees _employeeLookupService;
    private readonly IManageEmployees _employeeManager;

    public EmployeesController(ILookupEmployees employeeLookupService, IManageEmployees employeeManager)
    {
        _employeeLookupService = employeeLookupService;
        _employeeManager = employeeManager;
    }

    [HttpGet("/employees/{employeeId}/contact-information/home")]
    public async Task<ActionResult<ContactItem>> GetEmployeeHomeContactInfo(string employeeId)
    {
        ContactItem? response = await _employeeLookupService.GetEmployeeContactInfoForHomeAsync(employeeId);
        
        return response is null ? NotFound() :Ok(response);
    }


    [HttpPut("/employees/{employeeId}/contact-information/home")]
    public async Task<ActionResult> UpdateHomeContactInformation([FromRoute] string employeeId, [FromBody] HomeContactItem contactItem)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }
        //Makes sure it passed the validation first.
        bool foundAndUpated = await _employeeManager.UpdateContactInfoAsync(employeeId, contactItem);
        return foundAndUpated ? Ok(contactItem) : NotFound();
    }

    [HttpGet("/employees/{employeeId}/contact-information/work")]
    public async Task<ActionResult<ContactItem>> GetEmployeeWorkContactInfo(string employeeId)
    {
        ContactItem? response = await _employeeLookupService.GetEmployeeContactInfoForWorkAsync(employeeId);

        return response is null ? NotFound() : Ok(response);
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
