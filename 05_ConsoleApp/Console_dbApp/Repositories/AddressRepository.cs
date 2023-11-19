using Console_dbApp.Contexts;
using Console_dbApp.Models.Entities;

namespace Console_dbApp.Repositories;

public class AddressRepository : GenericRepository<AddressEntity>
{
    private readonly DataContext _context;

    public AddressRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}

