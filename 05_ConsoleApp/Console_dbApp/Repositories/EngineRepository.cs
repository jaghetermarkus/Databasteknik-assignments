using Console_dbApp.Contexts;
using Console_dbApp.Models.Entities;

namespace Console_dbApp.Repositories;

public class EngineRepository : GenericRepository<EngineEntity>
{
    private readonly DataContext _context;

    public EngineRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}

