using AutoMapper;
using EmployeesApi;
using EmployeesApi.Controllers.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.Domain;

public class EntityFrameworkEmployeeLookup : ILookupEmployees, IManageEmployees
{
    private readonly EmployeeDataContext _context;
    private readonly IMapper _mapper;
    private readonly MapperConfiguration _config;

    public EntityFrameworkEmployeeLookup(EmployeeDataContext context, IMapper mapper, MapperConfiguration config)
    {
        _context = context;
        _mapper = mapper;
        _config = config;
    }

    public async Task<EmployeeResponse?> GetEmployeeByIdAsync(string employeeId)
    {
        var id = int.Parse(employeeId);

        var employee = await _context.Employees
            .Where(e => e.Id == id)
            .SingleOrDefaultAsync(); // this will return 0 or 1, if it returns mroe than 1 BLOW UP


        if (employee is null)
        {
            return null;
        }
        return _mapper.Map<EmployeeResponse>(employee);

        //return new EmployeeResponse(employee.Id.ToString(), new NameInformation(employee.FirstName, employee.LastName), new WorkDetails(employee.Department),
        //        new Dictionary<string, Dictionary<string, string>>
        //        {
        //                            { "home", new Dictionary<string, string> { { "email", employee.HomeEmail}, { "phone", employee.HomePhone } } },
        //                            { "work", new Dictionary<string, string> {{ "email", employee.WorkEmail}, {  "phone", employee.WorkPhone } } },
        //        }
        //    );
    }

    public async Task<ContactItem?> GetEmployeeContactInfoForHomeAsync(string employeeId)
    {
        var id = int.Parse(employeeId);
        var response = await _context.Employees.Where(emp => emp.Id == id)
        .Select(emp => new ContactItem { Email = emp.HomeEmail, Phone = emp.HomePhone })
        .SingleOrDefaultAsync(); return response;
    }

    public async Task<ContactItem?> GetEmployeeContactInfoForWorkAsync(string employeeId)
    {
        var id = int.Parse(employeeId);
        var response = await _context.Employees.Where(emp => emp.Id == id)
        .Select(emp => new ContactItem { Email = emp.WorkEmail, Phone = emp.WorkPhone })
        .SingleOrDefaultAsync(); return response;
    }

    public async Task<bool> UpdateContactInfoAsync(string employeeId, HomeContactItem contactItem)
    {
        var id = int.Parse(employeeId);
        var employee = await _context.Employees.SingleOrDefaultAsync(emp => emp.Id == id); 
        
        if (employee != null)
        {
            employee.HomePhone = contactItem.Phone;
            employee.HomeEmail = contactItem.Email;             // NOTHING WILL HAPPEN UNLESS YOU DO THIS.
            await _context.SaveChangesAsync();
            return true;
        };
        return false;
    }
}