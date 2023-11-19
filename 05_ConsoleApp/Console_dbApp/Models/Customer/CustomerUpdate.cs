using Console_dbApp.Models.Entities;
using System.Diagnostics;

namespace Console_dbApp.Models.Customer;

public class CustomerUpdate
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

    public string? Instagram { get; set; }
    public string? Twitter { get; set; }
    public string? Facebook { get; set; }

    public static implicit operator CustomerUpdate(CustomerEntity entity)
    {
        try
        {
            return new CustomerUpdate
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                StreetName = entity.Address.StreetName,
                StreetNumber = entity.Address.StreetNumber,
                ZipCode = entity.Address.ZipCode,
                City = entity.Address.City,
                PhoneNumber = entity.ContactInformation.PhoneNumber,
                Email = entity.ContactInformation.Email,
            };
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}

