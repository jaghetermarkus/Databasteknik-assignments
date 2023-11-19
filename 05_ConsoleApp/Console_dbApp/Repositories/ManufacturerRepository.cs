using Console_dbApp.Contexts;
using Console_dbApp.Models.Entities;

namespace Console_dbApp.Repositories;

public class ManufacturerRepository : GenericRepository<ManufacturerEntity>
{
    private readonly DataContext _context;

    public ManufacturerRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}

