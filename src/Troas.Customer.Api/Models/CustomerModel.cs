using System.ComponentModel.DataAnnotations;

namespace Troas.Customer.Api.Models;

public class CustomerModel
{
    [Required]
    public string FirstName { get; set; }
    public string? MiddleName { get; set; } 
    [Required]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; }
    [Required]
    [DataType(DataType.PhoneNumber, ErrorMessage = "Not a valid phone number")]
    public string PhoneNumber { get; set; }
}