﻿using WebAPI.Model;
using WebAPI.ViewModels.Accounts;
using WebAPI.ViewModels.Login;

namespace WebAPI.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account>
    {
        AccountEmpVM Login(LoginVM loginVM);
        int Register(RegisterVM registerVM);
        int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM);
        int UpdateOTP(Guid? employeeId);
    }
}
