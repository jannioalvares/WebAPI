using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Repositories;
using WebAPI.ViewModels.AccountRoles;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : ControllerBase
    {

        private readonly IAccountRoleRepository _accountroleRepository;
        private readonly IMapper<AccountRole, AccountRoleVM> _mapper;
        public AccountRoleController(IAccountRoleRepository accountRoleRepository, IMapper<AccountRole, AccountRoleVM> mapper)
        {
            _accountroleRepository = accountRoleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accountroles = _accountroleRepository.GetAll();
            if (!accountroles.Any())
            {
                return NotFound();
            }

            var data = accountroles.Select(_mapper.Map).ToList();
            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var accountrole = _accountroleRepository.GetByGuid(guid);
            if (accountrole is null)
            {
                return NotFound();
            }

            var data = _mapper.Map(accountrole);
            return Ok(accountrole);
        }

        [HttpPost]
        public IActionResult Create(AccountRoleVM accountroleVM)
        {
            var accountroleConverted = _mapper.Map(accountroleVM);
            var result = _accountroleRepository.Create(accountroleConverted);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(AccountRoleVM accountroleVM)
        {
            var accountroleConverted = _mapper.Map(accountroleVM);
            var isUpdated = _accountroleRepository.Update(accountroleConverted);
            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _accountroleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
