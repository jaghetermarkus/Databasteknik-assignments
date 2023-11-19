using Console_dbApp.Models.Entities;
using System.Diagnostics;

namespace Console_dbApp.Models.Customer;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string StreetName { get; set; } = null!;
    public string? StreetNumber { get; set; }
    public string ZipCode { get; set; } = null!;
    public string City { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string? FullName => $"{FirstName} {LastName}";
    public string? FullContactInfo => $"<{Email}> {PhoneNumber}";
    public string? FullAddress => $"{StreetName} {StreetNumber}, {ZipCode} {City}";

    public static implicit operator Customer(CustomerEntity entity)
    {
        try
        {
            return new Customer
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                StreetName = entity.Address?.StreetName ?? "",
                StreetNumber = entity.Address?.StreetNumber,
                ZipCode = entity.Address?.ZipCode ?? "",
                City = entity.Address?.City ?? "",
                PhoneNumber = entity.ContactInformation?.PhoneNumber ?? "",
                Email = entity.ContactInformation?.Email ?? ""
                
            };
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

}

