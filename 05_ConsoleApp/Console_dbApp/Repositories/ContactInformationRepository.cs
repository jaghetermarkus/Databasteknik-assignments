using Console_dbApp.Contexts;
using Console_dbApp.Models.Entities;

namespace Console_dbApp.Repositories;

public class ContactInformationRepository : GenericRepository<ContactInformationEntity>
{
    private readonly DataContext _context;

    public ContactInformationRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}

