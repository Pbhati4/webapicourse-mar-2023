using EmployeesApi.Domain;

namespace EmployeesAPI.Controllers;

public class HiringRequestController : ControllerBase   
{
    private readonly EmployeeDataContext _context;

    public HiringRequestController(EmployeeDataContext context)
    {
        _context = context;
    }

    [HttpPost("/hiring-requests")]
    public async Task<ActionResult> AddHiringRequest([FromBody] HiringRequestModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var hiringEntity = new HiringRequestEntity
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Note = request.Note ?? "",
            CreatedAt = DateTime.UtcNow,
            Status = HiringRequestStatus.PendingDepartment
        };
        _context.HiringRequests.Add(hiringEntity);
        await _context.SaveChangesAsync(); return Ok(hiringEntity);
    }


}
