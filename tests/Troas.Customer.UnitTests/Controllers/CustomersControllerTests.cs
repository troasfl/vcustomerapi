using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Troas.Customer.Api.Controllers;
using Troas.Customer.Api.Models;
using Troas.Customer.Application.DbServices;

namespace Troas.Customer.UnitTests.Controllers;

public class CustomersControllerTests
{
    private readonly CustomersController _controller;
    private readonly Mock<ICustomerService> _customerServiceMock;

    public CustomersControllerTests()
    {
        _customerServiceMock = new Mock<ICustomerService>();
        Mock<ILogger<CustomersController>> loggerMock = new();
        _controller = new CustomersController(_customerServiceMock.Object, loggerMock.Object);
    }

    [Fact]
    public async Task CreateCustomer_ValidCustomer_ReturnsCreatedAtAction()
    {
        // Arrange
        var customerModel = new CustomerModel { FirstName = "Fredrick", LastName = "Lutterodt" };
        var customer = new Domain.Customer
        {
            Id = Guid.NewGuid(),
            FirstName = "Fredrick",
            LastName = "Lutterodt",
            MiddleName = "Troas",
            EmailAddress = "troasfl@gmail.com",
            PhoneNumber = "2175006661",
            CreatedAt = DateTime.UtcNow
        };
        _customerServiceMock.Setup(service => service.CreateCustomerAsync(customer)).ReturnsAsync(customer);

        // Act
        var result = await _controller.CreateCustomer(customerModel);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(customerModel.FirstName, (actionResult.Value as Domain.Customer)?.FirstName);
    }
}