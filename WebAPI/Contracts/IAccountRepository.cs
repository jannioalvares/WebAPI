using WebAPI.Model;
using WebAPI.ViewModels.Accounts;

namespace WebAPI.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account>
    {
        LoginVM Login(LoginVM loginVM);
        int Register(RegisterVM registerVM);
        int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM);
        int UpdateOTP(Guid? employeeId);

        IEnumerable<string> GetRoles(Guid guid);
    }
}
