using System.Diagnostics;
using System.Linq.Expressions;
using Console_dbApp.Contexts;
using Console_dbApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Console_dbApp.Repositories;

public class CarRepository : GenericRepository<CarEntity>, ICarRepository
{
    private readonly DataContext _context;

    public CarRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    // GET ALL - Return all cars with connected entities
    public override async Task<IEnumerable<CarEntity>> ReadAsync()
    {
        var cars = await _context
            .Cars
            .Include(x => x.Engine)
            .Include(x => x.Category)
            .Include(x => x.Manufacturer)
            .Include(x => x.Color)
            .Include(x => x.ModelYear)
            .ToListAsync();
        return cars;
    }

    // GET ALL - Based on their 'IsActive' state
    public async Task<IEnumerable<CarEntity>> ReadAsync(bool isActive)
    {
        var cars = await _context
            .Cars
            .Where(x => x.IsActive == isActive)
            .Include(x => x.Engine)
            .Include(x => x.Category)
            .Include(x => x.Manufacturer)
            .Include(x => x.Color)
            .Include(x => x.ModelYear)
            .ToListAsync();
        return cars;
    }

    // GET ONE - Based on the provided car ID 
    public async Task<CarEntity> ReadAsync(int carId)
    {
        try
        {
            var carEntity = await _context.Cars.FindAsync(carId);
            return carEntity!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    // TOGGLE ACTIVE STATE - Changes the 'IsActive' state of a car
    public virtual async Task<bool> ChangeActiveStateAsync(Expression<Func<CarEntity, bool>> expression)
    {
        try
        {
            // Retrieve the car entity matching the provided expression.
            var entity = await _context.Set<CarEntity>().FirstOrDefaultAsync(expression);

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

