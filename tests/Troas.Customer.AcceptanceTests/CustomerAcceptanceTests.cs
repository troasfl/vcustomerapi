using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Troas.Customer.Api.Models;
using Troas.Customer.Application.HelperServices;
using Program = Troas.Customer.Api.Program;

namespace Troas.Customer.AcceptanceTests;

public class CustomerAcceptanceTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CustomerAcceptanceTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateCustomer_WithValidData_Returns201Created()
    {
        // Arrange
        var newCustomer = new CustomerModel
        {
            FirstName = "Fredrick",
            LastName = "Lutterodt",
            EmailAddress = $"{Guid.NewGuid()}@gmail.com",
            PhoneNumber = NumberGenerator.GenerateRandomPhoneNumber(),
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/customers", newCustomer);

        // Assert
        response.EnsureSuccessStatusCode(); 
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task CreateCustomer_WithoutLastName_Returns400BadRequest()
    {
        // Arrange
        var newCustomer = new CustomerModel
        {
            FirstName = "Fredrick",
            // LastName is missing
            EmailAddress = "john.doe@example.com",
            PhoneNumber = "2175556661"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/customers", newCustomer);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}