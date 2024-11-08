namespace Troas.Customer.Domain;

public class Customer
{
    /// <summary>
    /// Automatically generates UUID
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// First Name
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Nullable middle name
    /// </summary>
    public string? MiddleName { get; set; } 
    
    /// <summary>
    /// Last Name
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// Must be unique
    /// </summary>
    public string EmailAddress { get; set; }
    
    /// <summary>
    /// Can be composite but will keep it simple and make it one number per customer
    /// </summary>
    public string PhoneNumber { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// 
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}