using System.Diagnostics;
using Console_dbApp.Models.Customer;
using Console_dbApp.Models.Entities;
using Console_dbApp.Repositories;

namespace Console_dbApp.Services;

public class CustomerService
{
    private readonly CustomerRepository _customerRepo;
    private readonly AddressRepository _addressRepo;
    private readonly ContactInformationRepository _contactInformationRepo;

    public CustomerService(CustomerRepository customerRepo, AddressRepository addressRepo, ContactInformationRepository contactInformationRepo)
    {
        _customerRepo = customerRepo;
        _addressRepo = addressRepo;
        _contactInformationRepo = contactInformationRepo;
    }

    // CREATE Customer
    public async Task<(CustomerEntity?, string)> CreateCustomerAsync(CustomerRegistration registration)
    {
        try
        {
            // Instanciate new customerEntity and add the properties from the input registration
            CustomerEntity customerEntity = registration;

            // Check if the email already exists in the database
            if (! await _contactInformationRepo.ExistsAsync(x => x.Email == customerEntity.ContactInformation.Email))
            {

                // If address already exists, add properties including Id to the new customerEntity
                var addressEntity = await _addressRepo.ReadAsync(x => x.StreetName == customerEntity.Address.StreetName && x.ZipCode == customerEntity.Address.ZipCode && x.StreetNumber == customerEntity.Address.StreetNumber);
                if (addressEntity != null)
                    customerEntity.Address = addressEntity;

                // Create the customer in the database
                CustomerEntity customerCreated =  await _customerRepo.CreateAsync(customerEntity);

                return (customerCreated, "NY KUND HAR LAGTS TILL:");
            }
            else
            {
                // If Email already exists, return null and message about conflict
                return (null, "Mejladressen finns redan, kunden kunde inte skapas" );
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        // If creating customer fails, return null and message about error
        return (null, "Ett fel uppstod, kunden skapades inte..");

    }

    // GET all customers
    public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        try
        {
            var customers = await _customerRepo.ReadAsync();
            return customers.ToList();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    // GET all customers based on 'IsActiv' state
    public async Task<IEnumerable<CustomerEntity>> GetAllAsync(bool input)
    {
        try
        {
            var customers = await _customerRepo.ReadAsync(isActive: input);
            return customers.ToList();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    // GET one customer based om provided customerId
    public async Task<CustomerEntity> GetCustomerAsync(int customerId)
    {
        try
        {
            return await _customerRepo.ReadAsync(customerId);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    // DELETE one customer based om provided customerId
    public async Task<bool> RemoveCustomerAsync(int customerId)
    {
        try
        {
            return await _customerRepo.DeleteAsync(x => x.Id == customerId);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false!;
    }

    // SOFT-DELETE - Changes the 'IsActive' state of a customer
    public async Task<bool> ChangeIsActiveAsync(int customerId)
    {
        try
        {
            return await _customerRepo.ChangeActiveStateAsync(x => x.Id == customerId);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false!;
    }

    // UPDATE customer. Receive the customerId and new properties from CustomerRegistration
    public async Task UpdateCustomerAsync(int customerId, CustomerRegistration update)
    {
        try
        {
            // Retrieve the existing customer based on the provided customerId
            var existingCustomer = await _customerRepo.ReadAsync(customerId);

            if (existingCustomer != null)
            {
                // Check each provided property; if empty, use the existing one, otherwise, update to the new value
                existingCustomer.FirstName = string.IsNullOrWhiteSpace(update.FirstName) ? existingCustomer.FirstName : update.FirstName;
                existingCustomer.LastName = string.IsNullOrWhiteSpace(update.LastName) ? existingCustomer.LastName : update.LastName;
                existingCustomer.ContactInformation.Email = string.IsNullOrWhiteSpace(update.Email) ? existingCustomer.ContactInformation.Email : update.Email;
                existingCustomer.ContactInformation.PhoneNumber = string.IsNullOrWhiteSpace(update.PhoneNumber) ? existingCustomer.ContactInformation.PhoneNumber : update.PhoneNumber;
                existingCustomer.Address.StreetName = string.IsNullOrWhiteSpace(update.StreetName) ? existingCustomer.Address.StreetName : update.StreetName;
                existingCustomer.Address.StreetNumber = string.IsNullOrWhiteSpace(update.StreetNumber) ? existingCustomer.Address.StreetNumber : update.StreetNumber;
                existingCustomer.Address.ZipCode = string.IsNullOrWhiteSpace(update.ZipCode) ? existingCustomer.Address.ZipCode : update.ZipCode;
                existingCustomer.Address.City = string.IsNullOrWhiteSpace(update.City) ? existingCustomer.Address.City : update.City;

                // Update the customer in the database
                await _customerRepo.UpdateAsync(existingCustomer);
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

    }
}