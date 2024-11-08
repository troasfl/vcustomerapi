using Microsoft.EntityFrameworkCore;

namespace Troas.Customer.Infrastructure.Persistence;

public class CustomerRepository(AppDbContext dbContext) : ICustomerRepository
{
    public async Task AddCustomerAsync(Domain.Customer customer)
    {
        await dbContext.Customers.AddAsync(customer);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Domain.Customer>> GetAllCustomersAsync()
    {
        return await dbContext.Customers.ToListAsync();
    }
    
    public async Task<Domain.Customer> GetByIdAsync(Guid customerId)
    {
        var customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id.Equals(customerId));
        return customer;
    }

    public async Task UpdateCustomerAsync(Domain.Customer customer)
    {
        dbContext.Customers.Update(customer); 
        await dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(Guid customerId)
    {
        var customer = await GetByIdAsync(customerId);
        dbContext.Customers.Remove(customer);
        await dbContext.SaveChangesAsync();
    }
}