using Azure;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Others;
using WebAPI.Repositories;
using WebAPI.ViewModels.AccountRoles;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : BaseController<AccountRole, AccountRoleVM>
    {

        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IMapper<AccountRole, AccountRoleVM> _mapper;
        public AccountRoleController(IAccountRoleRepository accountRoleRepository,
                IMapper<AccountRole,
                AccountRoleVM> mapper) : base(accountRoleRepository, mapper)
        {
            _accountRoleRepository = accountRoleRepository;
            _mapper = mapper;
        }
    }
}
