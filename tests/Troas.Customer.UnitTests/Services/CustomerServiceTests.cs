using Moq;
using Troas.Customer.Application.DbServices;
using Troas.Customer.Infrastructure.Persistence;

namespace Troas.Customer.UnitTests.Services;

public class CustomerServiceTests
{
    private readonly CustomerService _customerService;
    private readonly Mock<ICustomerRepository> _mockCustomerRepository;

    public CustomerServiceTests()
    {
        _mockCustomerRepository = new Mock<ICustomerRepository>();
        _customerService = new CustomerService(_mockCustomerRepository.Object);
    }

    [Fact]
    public async Task CreateCustomer_ShouldReturnNewCustomer()
    {
        // Arrange
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
        _mockCustomerRepository.Setup(repo => repo.AddCustomerAsync(It.IsAny<Domain.Customer>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _customerService.CreateCustomerAsync(customer);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customer.FirstName, result.FirstName);
        Assert.Equal(customer.LastName, result.LastName);
        Assert.Equal(customer.MiddleName, result.MiddleName);
        Assert.Equal(customer.EmailAddress, result.EmailAddress);
        Assert.Equal(customer.PhoneNumber, result.PhoneNumber);
        Assert.Equal(customer.CreatedAt, result.CreatedAt);
        _mockCustomerRepository.Verify(repo => repo.AddCustomerAsync(customer), Times.Once);
    }
}