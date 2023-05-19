using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;

namespace WebAPI.Repositories
{
    public class AccountRoleRepository : IAccountRoleRepository
    {
        private readonly BookingManagementDbContext _context;
        public AccountRoleRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public AccountRole Create(AccountRole accountrole)
        {
            try
            {
                _context.Set<AccountRole>().Add(accountrole);
                _context.SaveChanges();
                return accountrole;
            }
            catch
            {
                return new AccountRole();
            }
        }

        public bool Update(AccountRole accountrole)
        {
            try
            {
                _context.Set<AccountRole>().Update(accountrole);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Guid guid)
        {
            try
            {
                var accountrole = GetByGuid(guid);
                if (accountrole == null)
                {
                    return false;
                }

                _context.Set<AccountRole>().Remove(accountrole);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<AccountRole> GetAll()
        {
            return _context.Set<AccountRole>().ToList();
        }

        public AccountRole? GetByGuid(Guid guid)
        {
            return _context.Set<AccountRole>().Find(guid);
        }
    }
}
