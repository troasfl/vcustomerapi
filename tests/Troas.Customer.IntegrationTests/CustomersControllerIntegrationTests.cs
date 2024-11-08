using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Troas.Customer.Api.Models;
using Troas.Customer.Infrastructure.Persistence;

namespace Troas.Customer.IntegrationTests;

public class CustomersControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    private readonly HttpClient _client;
    private readonly AppDbContext _context;

    public CustomersControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        var scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
        var scope = scopeFactory.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }

    [Fact]
    public async Task CreateCustomer_ReturnsCreatedAtAction()
    {
        // Arrange
        var customerModel = new CustomerModel
        {
            FirstName = "Fredrick",
            LastName = "Lutterodt",
            MiddleName = "T",
            EmailAddress = "fred.lutt@gmail.com",
            PhoneNumber = "217555661"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/customers", customerModel);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
    
    /// <summary>
    /// Clean up records created during the test
    /// </summary>
    public void Dispose()
    {
        _context.Customers.RemoveRange(_context.Customers);
        _context.SaveChangesAsync();
            
        // Dispose the HttpClient
        _client.Dispose();
    }
}