using WebAPI.Model;

namespace WebAPI.Contracts
{
    public interface IRoleRepository
    {
        Role Create(Role role);
        bool Update(Role role);
        bool Delete(Guid guid);
        IEnumerable<Role> GetAll();
        Role? GetByGuid(Guid guid);
    }
}
