using System.Text.RegularExpressions;

namespace Troas.Customer.Infrastructure.Persistence;

public interface ICustomerRepository
{
    Task AddCustomerAsync(Domain.Customer customer);
    Task<List<Domain.Customer>> GetAllCustomersAsync();
    Task<Domain.Customer> GetByIdAsync(Guid customerId);
    Task UpdateCustomerAsync(Domain.Customer customer);
    Task DeleteAsync(Guid customerId);
}