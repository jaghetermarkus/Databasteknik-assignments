using Console_dbApp.Contexts;
using Console_dbApp.Models.Entities;

namespace Console_dbApp.Repositories;

public class CategoryRepository : GenericRepository<CategoryEntity>
{
    private readonly DataContext _context;

    public CategoryRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}

