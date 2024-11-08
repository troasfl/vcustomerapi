using Troas.Customer.Infrastructure.Persistence;

namespace Troas.Customer.Application.DbServices;

public class CustomerService(ICustomerRepository customerRepository): ICustomerService
{
    public async Task<Domain.Customer> CreateCustomerAsync(Domain.Customer customer)
    {
        await customerRepository.AddCustomerAsync(customer);
        return customer;
    }

    public async Task<List<Domain.Customer>> GetAllCustomersAsync()
    {
        return await customerRepository.GetAllCustomersAsync();
    }
    
    public async Task<Domain.Customer> GetCustomerByIdAsync(Guid customerId)
    {
        var customer = await customerRepository.GetByIdAsync(customerId);
        return customer;
    }
    
    public async Task UpdateCustomerAsync(Domain.Customer customer)
    {
        await customerRepository.UpdateCustomerAsync(customer);
    }

    public async Task DeleteCustomerAsync(Guid customerId)
    {
        await customerRepository.DeleteAsync(customerId);
    }
}