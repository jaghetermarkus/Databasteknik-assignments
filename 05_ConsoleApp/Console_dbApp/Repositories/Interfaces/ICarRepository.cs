using System.Linq.Expressions;
using Console_dbApp.Models.Entities;

namespace Console_dbApp.Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<CarEntity>> ReadAsync();
        Task<CarEntity> ReadAsync(int carId);
        Task<bool> ChangeActiveStateAsync(Expression<Func<CarEntity, bool>> expression);
        Task<IEnumerable<CarEntity>> ReadAsync(bool isActive);
    }
}