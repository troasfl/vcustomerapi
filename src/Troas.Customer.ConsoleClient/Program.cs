using Troas.Customer.ConsoleClient;
using Troas.Customer.Domain;

class Program
{
    private static async Task Main(string[] args)
    {
        var httpClient = new HttpClient();
        var apiClient = new CustomerApiClient(httpClient);

        bool running = true;

        while (running)
        {
            Console.WriteLine("\nCustomer Management System");
            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. Get All Customers");
            Console.WriteLine("3. Get Customer by ID");
            Console.WriteLine("4. Update Customer");
            Console.WriteLine("5. Delete Customer");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateCustomer(apiClient);
                    break;
                case "2":
                    await GetAllCustomers(apiClient);
                    break;
                case "3":
                    await GetCustomerById(apiClient);
                    break;
                case "4":
                    await UpdateCustomer(apiClient);
                    break;
                case "5":
                    await DeleteCustomer(apiClient);
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }

    private static async Task CreateCustomer(CustomerApiClient apiClient)
    {
        Console.WriteLine("Creating a new customer...");
        var newCustomer = new Customer();

        Console.Write("Enter First Name: ");
        newCustomer.FirstName = Console.ReadLine();

        Console.Write("Enter Last Name: ");
        newCustomer.LastName = Console.ReadLine();

        Console.Write("Enter Email Address: ");
        newCustomer.EmailAddress = Console.ReadLine();
        
        Console.Write("Enter Phone Number: ");
        newCustomer.PhoneNumber = Console.ReadLine();
        
        await apiClient.CreateCustomerAsync(newCustomer);
        Console.WriteLine("Customer created successfully.");
    }

    private static async Task GetAllCustomers(CustomerApiClient apiClient)
    {
        Console.WriteLine("Fetching all customers...");
        var customers = await apiClient.GetCustomersAsync();
        Console.WriteLine("Customers List:");
        foreach (var customer in customers)
        {
            Console.WriteLine($"- {customer.Id}: {customer.FirstName} {customer.LastName} ({customer.EmailAddress})");
        }
    }

    private static async Task GetCustomerById(CustomerApiClient apiClient)
    {
        Console.Write("Enter Customer ID: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            var customer = await apiClient.GetCustomerAsync(id);
            if (customer != null)
            {
                Console.WriteLine($"Customer Details: {customer.FirstName} {customer.LastName} ({customer.EmailAddress})");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID format.");
        }
    }

    private static async Task UpdateCustomer(CustomerApiClient apiClient)
    {
        Console.Write("Enter Customer ID to update: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            var existingCustomer = await apiClient.GetCustomerAsync(id);
            if (existingCustomer != null)
            {
                Console.WriteLine("Updating customer...");
                Console.Write("Enter New First Name (leave blank to keep current): ");
                string firstName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(firstName))
                {
                    existingCustomer.FirstName = firstName;
                }

                Console.Write("Enter New Last Name (leave blank to keep current): ");
                string lastName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(lastName))
                {
                    existingCustomer.LastName = lastName;
                }

                Console.Write("Enter New Email Address (leave blank to keep current): ");
                string email = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(email))
                {
                    existingCustomer.EmailAddress = email;
                }

                await apiClient.UpdateCustomerAsync(id, existingCustomer);
                Console.WriteLine("Customer updated successfully.");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID format.");
        }
    }

    private static async Task DeleteCustomer(CustomerApiClient apiClient)
    {
        Console.Write("Enter Customer ID to delete: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            await apiClient.DeleteCustomerAsync(id);
            Console.WriteLine("Customer deleted successfully.");
        }
        else
        {
            Console.WriteLine("Invalid ID format.");
        }
    }
}
