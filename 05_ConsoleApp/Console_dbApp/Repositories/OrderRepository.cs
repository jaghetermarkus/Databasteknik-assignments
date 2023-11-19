using System.Diagnostics;
using Console_dbApp.Contexts;
using Console_dbApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Console_dbApp.Repositories;

public class OrderRepository : GenericRepository<OrderEntity>
{
    private readonly DataContext _context;

    public OrderRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    // GET ALL ORDERS including connected entities
    public override async Task<IEnumerable<OrderEntity>> ReadAsync()
    {
        try
        {
            var orders = await _context
                .Orders
                .Include(x => x.CustomerOrders)
                .ThenInclude(x => x.Customer)
                .Include(x => x.Car)
                .Include(x => x.Car.Engine)
                .Include(x => x.Car.Category)
                .Include(x => x.Car.Manufacturer)
                .Include(x => x.Car.ModelYear)
                .Include(x => x.Car.Color)
                .ToListAsync();
            return orders;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }
}

