using Microsoft.AspNetCore.Mvc;
using Troas.Customer.Api.Models;
using Troas.Customer.Application.DbServices;

namespace Troas.Customer.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomersController(ICustomerService customerService, ILogger<CustomersController> logger)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CustomerModel customerModel)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Invalid customer details");
            return BadRequest();
        }
        var customer = new Domain.Customer
        {
            FirstName = customerModel.FirstName,
            MiddleName = customerModel.MiddleName,
            LastName = customerModel.LastName,
            EmailAddress = customerModel.EmailAddress,
            PhoneNumber = customerModel.PhoneNumber
        };
        var result = await customerService.CreateCustomerAsync(customer);
        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        var customers = await customerService.GetAllCustomersAsync();
        return Ok(customers);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Domain.Customer>> GetCustomer(Guid id)
    {
        var customer = await customerService.GetCustomerByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(Guid id, CustomerModel customerModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var existingCustomer = await customerService.GetCustomerByIdAsync(id);
        if (existingCustomer == null)
        {
            return NotFound();
        }
        existingCustomer.FirstName = customerModel.FirstName;
        existingCustomer.LastName = customerModel.LastName;
        existingCustomer.MiddleName = customerModel.MiddleName;
        existingCustomer.PhoneNumber = customerModel.PhoneNumber;
        existingCustomer.EmailAddress = customerModel.EmailAddress;
        await customerService.UpdateCustomerAsync(existingCustomer);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        await customerService.DeleteCustomerAsync(id);
        return NoContent();
    }
}