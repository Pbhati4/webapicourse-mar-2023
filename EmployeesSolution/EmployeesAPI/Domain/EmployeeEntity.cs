namespace EmployeesAPI.Domain;

public class EmployeeEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string HomePhone { get; set; } = string.Empty;
    public string HomeEmail { get; set; } = string.Empty;
    public string WorkPhone { get; set; } = string.Empty;
    public string WorkEmail { get; set; } = string.Empty;

}
