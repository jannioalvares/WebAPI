using System.Linq.Expressions;
using WebAPI.Model;

namespace WebAPI.Contracts
{
    public interface IRepository<Entity> where Entity : class
    {
        Entity Create(Entity entity);
        bool Update(Entity entity);
        bool Delete(Guid guid);
        IEnumerable<Entity> GetAll();
        Entity GetByGuid(Guid guid);
    }
}
