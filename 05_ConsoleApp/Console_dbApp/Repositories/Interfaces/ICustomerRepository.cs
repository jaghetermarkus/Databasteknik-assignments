using System.Linq.Expressions;
using Console_dbApp.Models.Entities;

namespace Console_dbApp.Repositories
{
    public interface ICustomerRepository : IGenericRepository<CustomerEntity>
    {
        new Task<IEnumerable<CustomerEntity>> ReadAsync();
        Task<CustomerEntity> ReadAsync(int customerId);
        Task<bool> ChangeActiveStateAsync(Expression<Func<CustomerEntity, bool>> expression);
        Task<IEnumerable<CustomerEntity>> ReadAsync(bool isActive);
    }
}