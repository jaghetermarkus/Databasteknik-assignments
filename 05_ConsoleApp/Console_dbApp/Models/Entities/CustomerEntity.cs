using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Console_dbApp.Models.Customer;

namespace Console_dbApp.Models.Entities;

public class CustomerEntity
{
    [Key]
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public int AddressId { get; set; }
    public AddressEntity Address { get; set; } = null!;

    public int ContactInformationId { get; set; }
    public ContactInformationEntity ContactInformation { get; set; } = null!;

    public ICollection<CustomerOrderEntity>? CustomerOrders { get; set; }

    public string? FullName => $"{FirstName} {LastName}";

    public static implicit operator CustomerEntity(CustomerRegistration registration)
    {
        try
        {
            return new CustomerEntity
            {
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                Address = new AddressEntity
                {
                    StreetName = registration.StreetName,
                    StreetNumber = registration.StreetNumber,
                    ZipCode = registration.ZipCode,
                    City = registration.City
                },
                ContactInformation = new ContactInformationEntity
                {
                    PhoneNumber = registration.PhoneNumber,
                    Email = registration.Email
                }
            };
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public static implicit operator CustomerEntity(CustomerUpdate update)
    {
        try
        {
            return new CustomerEntity
            {
                Id = update.Id,
                FirstName = update.FirstName,
                LastName = update.LastName,
                Address = new AddressEntity
                {
                    StreetName = update.StreetName,
                    StreetNumber = update.StreetNumber,
                    ZipCode = update.ZipCode,
                    City = update.City
                },
                ContactInformation = new ContactInformationEntity
                {
                    PhoneNumber = update.PhoneNumber,
                    Email = update.Email
                }
            };
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

}


