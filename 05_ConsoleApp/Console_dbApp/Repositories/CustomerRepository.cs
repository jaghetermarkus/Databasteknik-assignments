using System.Diagnostics;
using System.Linq.Expressions;
using Console_dbApp.Contexts;
using Console_dbApp.Models.Customer;
using Console_dbApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Console_dbApp.Repositories;

public class CustomerRepository : GenericRepository<CustomerEntity>, ICustomerRepository
{
    private readonly DataContext _context;

    public CustomerRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    // GET ALL - Return all customers including Address and CustomerInformationEntity
    public override async Task<IEnumerable<CustomerEntity>> ReadAsync()
    {
        try
        {
            var customers = await _context
                .Customers
                .Include(x => x.Address)
                .Include(x => x.ContactInformation)
                .ToListAsync();
            return customers;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    // GET ALL - Based on their 'IsActive' state
    public async Task<IEnumerable<CustomerEntity>> ReadAsync(bool isActive)
    {
        try
        {
            var customers = await _context
                .Customers
                .Where(x => x.IsActive == isActive)
                .Include(x => x.Address)
                .Include(x => x.ContactInformation)
                .ToListAsync();
            return customers;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    // GET ONE - Based on the provided customer ID 
    public async Task<CustomerEntity> ReadAsync(int customerId)
    {
        try
        {
            var customerEntity = await _context.Customers.FindAsync(customerId);
            return customerEntity!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }


    // TOGGLE ACTIVE STATE - Changes the 'IsActive' state of a customer
    public virtual async Task<bool> ChangeActiveStateAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        try
        {
            // Retrieve the customer entity matching the provided expression.
            var entity = await _context.Set<CustomerEntity>().FirstOrDefaultAsync(expression);

            // If the entity exists, toggle its 'IsActive' state and save changes to the database.
            if (entity != null)
            {
                entity.IsActive = !entity.IsActive;
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

}

