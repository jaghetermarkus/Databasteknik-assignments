using Console_dbApp.Contexts;
using Console_dbApp.Models.Entities;

namespace Console_dbApp.Repositories;

public class ModelYearRepository : GenericRepository<ModelYearEntity>
{
    private readonly DataContext _context;

    public ModelYearRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}

