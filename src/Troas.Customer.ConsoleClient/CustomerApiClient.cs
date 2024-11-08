using System.Net.Http.Json;

namespace Troas.Customer.ConsoleClient;

public class CustomerApiClient(HttpClient httpClient)
{
    private const string BaseUrl = "http://localhost:5182/api/v1";

    public async Task<List<Domain.Customer>?> GetCustomersAsync()
    {
        return await httpClient.GetFromJsonAsync<List<Domain.Customer>>($"{BaseUrl}/customers");
    }

    public async Task<Domain.Customer?> GetCustomerAsync(Guid id)
    {
        return await httpClient.GetFromJsonAsync<Domain.Customer>($"{BaseUrl}/customers/{id}");
    }

    public async Task CreateCustomerAsync(object customer)
    {
        var response = await httpClient.PostAsJsonAsync($"{BaseUrl}/customers", customer);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateCustomerAsync(Guid id, object customer)
    {
        var response = await httpClient.PutAsJsonAsync($"{BaseUrl}/customers/{id}", customer);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteCustomerAsync(Guid id)
    {
        var response = await httpClient.DeleteAsync($"{BaseUrl}/customers/{id}");
        response.EnsureSuccessStatusCode();
    }
}