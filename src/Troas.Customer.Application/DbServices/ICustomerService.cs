namespace Troas.Customer.Application.DbServices;

public interface ICustomerService
{
    Task<Domain.Customer> CreateCustomerAsync(Domain.Customer customer);
    Task<List<Domain.Customer>> GetAllCustomersAsync();
    Task<Domain.Customer> GetCustomerByIdAsync(Guid customerId);
    Task UpdateCustomerAsync(Domain.Customer customer);
    
    Task DeleteCustomerAsync(Guid customerId);
}