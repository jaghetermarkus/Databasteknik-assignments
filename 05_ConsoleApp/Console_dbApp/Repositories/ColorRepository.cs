using Console_dbApp.Contexts;
using Console_dbApp.Models.Entities;

namespace Console_dbApp.Repositories;

public class ColorRepository : GenericRepository<ColorEntity>
{
    private readonly DataContext _context;

    public ColorRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}

