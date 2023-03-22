using EmployeesApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.Controllers;

public class HiringRequestController : ControllerBase   
{
    private readonly EmployeeDataContext _context;

    public HiringRequestController(EmployeeDataContext context)
    {
        _context = context;
    }

    [HttpPost("/hiring-requests")]
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 10)]
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
        await _context.SaveChangesAsync();
        // A post to a collection should return a 201 Created, a Link to the new item, and a copy of that item.
        return CreatedAtRoute("hiringrequests-get-by-id", new { id = hiringEntity.Id }, hiringEntity);
    }

    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 10)]
    [HttpGet("/hiring-requests/{id:int}", Name = "hiringrequests-get-by-id")]
    public async Task<ActionResult> GetHiringRequest([FromRoute] int id)
    {
        var response = await _context.HiringRequests.SingleOrDefaultAsync(r => r.Id == id);
        return response is null ? NotFound() : Ok(response);
    }

}
