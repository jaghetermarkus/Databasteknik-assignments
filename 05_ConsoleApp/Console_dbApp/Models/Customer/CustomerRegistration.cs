namespace Console_dbApp.Models.Customer;

public class CustomerRegistration
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string StreetName { get; set; } = null!;
    public string? StreetNumber { get; set; }
    public string ZipCode { get; set; } = null!;
    public string City { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
}

