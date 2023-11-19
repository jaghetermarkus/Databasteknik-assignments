using Console_dbApp.Models.Address;
using Console_dbApp.Models.Customer;
using System.Diagnostics;

namespace Console_dbApp.Models.Entities;

public class AddressEntity
{
    public int Id { get; set; }
    public string StreetName { get; set; } = null!;
    public string? StreetNumber { get; set; }
    public string ZipCode { get; set; } = null!;
    public string City { get; set; } = null!;

    public string? FullAddress => $"{StreetName} {StreetNumber}, {ZipCode} {City}";

    public static implicit operator AddressEntity(AddressRegistration reg)
    {
        try
        {
            return new AddressEntity
            {
                StreetName = reg.StreetName,
                StreetNumber = reg.StreetNumber,
                ZipCode = reg.ZipCode,
                City = reg.City
            };
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

}

