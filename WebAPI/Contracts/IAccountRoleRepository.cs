using WebAPI.Model;

namespace WebAPI.Contracts
{
    public interface IAccountRoleRepository
    {
        AccountRole Create(AccountRole accountrole);
        bool Update(AccountRole accountrole);
        bool Delete(Guid guid);
        IEnumerable<AccountRole> GetAll();
        AccountRole? GetByGuid(Guid guid);
    }
}
