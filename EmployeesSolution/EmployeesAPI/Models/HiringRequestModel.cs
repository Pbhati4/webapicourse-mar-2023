using System.ComponentModel.DataAnnotations;

namespace EmployeesAPI.Models;

public class HiringRequestModel
{
    [Required]
    public string FirstName { get; init; } = string.Empty;
    [Required]
    public string LastName { get; init; } = string.Empty;
    [MaxLength(1000)]
    public string? Note { get; init; }
}
