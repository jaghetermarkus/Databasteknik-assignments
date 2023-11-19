namespace Console_dbApp.Models.Address;

public class AddressRegistration
{
    public string StreetName { get; set; } = null!;
    public string? StreetNumber { get; set; }
    public string ZipCode { get; set; } = null!;
    public string City { get; set; } = null!;
}

