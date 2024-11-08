namespace Troas.Customer.Application.HelperServices;

public static class NumberGenerator
{
    private static readonly Random Random = new();

    public static string GenerateRandomPhoneNumber()
    {
        // Generate a random phone number
        var phoneNumber = $"{Random.Next(100, 1000)}{Random.Next(100, 1000)}{Random.Next(1000, 10000)}";
        return phoneNumber;
    }

}