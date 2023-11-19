using Console_dbApp.Models.ContactInformation;
using Console_dbApp.Models.Customer;
using System.Diagnostics;

namespace Console_dbApp.Models.Entities;

public class ContactInformationEntity
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string? FullContactInfo => $"<{Email}> tel: {PhoneNumber}";

    public static implicit operator ContactInformationEntity(ContactInfoRegistration reg)
    {
        try
        {
            return new ContactInformationEntity
            {
                PhoneNumber = reg.PhoneNumber,
                Email = reg.Email,
            };
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}

