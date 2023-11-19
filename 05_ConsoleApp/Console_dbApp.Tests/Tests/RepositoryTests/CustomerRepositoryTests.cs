using Console_dbApp.Contexts;
using Console_dbApp.Models.Entities;
using Console_dbApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Console_dbApp.Tests.Tests.RepositoryTests;

public class CustomerRepositoryTests
{
    private async Task<DataContext> GetDataContext() // Creating the InMemory Database
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var databaseContext = new DataContext(options);
        await databaseContext.Database.EnsureCreatedAsync();

        return databaseContext;
    }


    [Fact]
    public async Task CreateAsync_Should_IfWorking_ReturnCustomerEntity()
    {
        // Arrange
        var customer = GetSampleCustomer()[0]; // Retrieve a sample customer from the list

        var dataContext = await GetDataContext(); // Instantiate the database context

        var customerRepository = new CustomerRepository(dataContext); // Create an instance of the customer repository

        // Act
        var result = customerRepository.CreateAsync(customer); // Create a new customer using the repository

        // Assert
        Assert.NotNull(result); // Ensure that the result is not null
        Assert.NotEqual(default(int), result.Id); // Check that the result has been assigned an Id (Id > 0)
    }

    [Fact]
    public async Task ReadAsync_Should_IfWorking_ReturnOneEntity()
    {
        // Arrange
        var customers = GetSampleCustomer(); // Retrieve a list of sample customers

        
        var dataContext = await GetDataContext(); // Instantiate the database context and add customer
        dataContext.Customers.AddRange(customers);
        await dataContext.SaveChangesAsync();

        var customerRepository = new CustomerRepository(dataContext); // Create an instance of the customer repository

        // Act
        var result = await customerRepository.ReadAsync(); // Retrieve customers from the repository

        // Assert
        Assert.True(customers != null);
        Assert.NotNull(result); 
        Assert.True(result.Any()); 
        Assert.True(result.Count() > 0);
        Assert.Equal(customers.Count, result.Count()); 

        // Compare the expected and actual lists of customers
        var expected = customers.OrderBy(c => c.FirstName).ThenBy(c => c.LastName).ToList();
        var actual = result.OrderBy(c => c.FirstName).ThenBy(c => c.LastName).ToList();

        for (int i = 0; i < expected.Count; i++)
        {
            // Ensure that each customer's FirstName and LastName match
            Assert.Equal(expected[i].FirstName, actual[i].FirstName);
            Assert.Equal(expected[i].LastName, actual[i].LastName);
        }
    }

    [Fact]
    public async Task DeleteAsync_Should_IfRemoveIsSuccessful_ReturnTrue()
    {
        // Arrange
        var customer = GetSampleCustomer().First(); // Retrieve a single sample customer from the list

        var dataContext = await GetDataContext(); // Instantiate the database context and add customer
        dataContext.Customers.Add(customer);
        await dataContext.SaveChangesAsync();

        var customerRepository = new CustomerRepository(dataContext); // Create an instance of the customer repository

        // Act
        var result = await customerRepository.DeleteAsync(x => x.Id == customer.Id); // Delete customer using the repository

        // Assert
        Assert.NotNull(customer); // Ensure that customer has been added from the list
        Assert.True(result); // Ensure that DeleteAsync generated did succed
    }

    [Fact]
    public async Task ExistsAsync_Should_WhenEmailDontExist_ReturnFalse()
    {
        // Arrange
        var customers = GetSampleCustomer(); // Retrieve a list of sample customers

        var dataContext = await GetDataContext(); // Instantiate the database context and add customer
        dataContext.Customers.AddRange(customers);
        await dataContext.SaveChangesAsync();

        var customerRepository = new CustomerRepository(dataContext); // Create an instance of the customer repository

        // Act
        var result = await customerRepository.ExistsAsync(x => x.ContactInformation.Email == $"{Guid.NewGuid()}");

        // Assert
        Assert.NotNull(customers);
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_IfWorking_ReturnNewEntity()
    {
        // Arrange
        var customer = GetSampleCustomer().First(); // Retrieve a single sample customer from the list

        var dataContext = await GetDataContext(); // Instantiate the database context and add customer
        dataContext.Customers.Add(customer);
        await dataContext.SaveChangesAsync();

        var customerRepository = new CustomerRepository(dataContext); // Create an instance of the customer repository
        customer.FirstName = "NewFirstName";

        // Act
        var result = await customerRepository.UpdateAsync(customer);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("NewFirstName", result.FirstName);

    }

    [Fact]
    public async Task ChangeActiveState_Should_WhenWorking_ReturnTrue()
    {
        // Arrange
        var customer = GetSampleCustomer().First(); // Retrieve a single sample customer from the list
        var isActiveBeforeChange = customer.IsActive; // Save the 'IsActive' state before the 'Act' is performed


        var dataContext = await GetDataContext(); // Instantiate the database context and add customer
        dataContext.Customers.Add(customer);
        await dataContext.SaveChangesAsync();

        var customerRepository = new CustomerRepository(dataContext); // Create an instance of the customer repository

        // Act
        var result = await customerRepository.ChangeActiveStateAsync(x => x.Id == customer.Id);

        // Assert
        Assert.True(result); // Ensure the method is succesfull 
        Assert.False(customer.IsActive); // Ensure that 'IsActive' has changed to false
        Assert.True(customer.IsActive != isActiveBeforeChange); // Ensure that IsActive is differens before and after Act si done

    }

    private List<CustomerEntity> GetSampleCustomer()

    {
        // Create and return a list of sample customers with associated address and contact information
        List<CustomerEntity> customers = new List<CustomerEntity>
        {
            new CustomerEntity
                {
                    FirstName = "Elon",
                    LastName = "Musk",
                    IsActive = true,
                    Address = new AddressEntity()
                {
                    StreetName = "Stigen",
                    StreetNumber = "1",
                    ZipCode = "12345",
                    City = "Storstaden"
                },
                ContactInformation = new ContactInformationEntity()
                {
                    Email = "elon@tesla.com",
                    PhoneNumber = "1-123 456"
                }
                },

                new CustomerEntity
                {
                    FirstName = "Bill",
                    LastName = "Gates",
                    IsActive = true,
                    Address = new AddressEntity()
                {
                    StreetName = "Vägen",
                    StreetNumber = "2",
                    ZipCode = "67890",
                    City = "Lilla staden"
                },
                ContactInformation = new ContactInformationEntity()
                {
                    Email = "bill@microsoft.com",
                    PhoneNumber = "1-456 123"
                }
                },

                new CustomerEntity
                {
                    FirstName = "Mark",
                    LastName = "Zuckerberg",
                    IsActive = true,
                    Address = new AddressEntity()
                {
                    StreetName = "Gatan",
                    StreetNumber = "3",
                    ZipCode = "11223",
                    City = "Byn"
                },
                ContactInformation = new ContactInformationEntity()
                {
                    Email = "mark@facebook.com",
                    PhoneNumber = "1-111 222"
                }
                },

                new CustomerEntity
                {
                    FirstName = "Steve",
                    LastName = "Jobs",
                    IsActive = true,
                    Address = new AddressEntity()
                {
                    StreetName = "Street",
                    StreetNumber = "4",
                    ZipCode = "44556",
                    City = "Samhället"
                },
                ContactInformation = new ContactInformationEntity()
                {
                    Email = "jobs@apple.com",
                    PhoneNumber = "1-777 555"
                }
                },

                new CustomerEntity
                {
                    FirstName = "Warren",
                    LastName = "Buffert",
                    IsActive = true,
                    Address = new AddressEntity()
                {
                    StreetName = "Road",
                    StreetNumber = "5",
                    ZipCode = "99999",
                    City = "Moneytown"
                },
                ContactInformation = new ContactInformationEntity()
                {
                    Email = "warren@buffert.com",
                    PhoneNumber = "1-999 999"
                }
                }
        };
        return customers;
    }

}

