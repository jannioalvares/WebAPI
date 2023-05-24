using System.Linq.Expressions;
using WebAPI.Model;

namespace WebAPI.Contracts
{
    public interface IGeneralRepository<TEntity>
    {
        TEntity? Create(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(Guid guid);
        IEnumerable<TEntity> GetAll();
        TEntity? GetByGuid(Guid guid);
    }
}
