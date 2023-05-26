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
            var response = new ResponseVM<IEnumerable<AccountRoleVM>>();
            var accountroles = _accountroleRepository.GetAll();
            if (!accountroles.Any())
            {
                return NotFound(response.NotFound("AccountRole not found"));
            }

            var data = accountroles.Select(_mapper.Map).ToList();
            return Ok(response.Success(data, "AccountRole Found"));
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var response = new ResponseVM<AccountRoleVM>();
            var accountrole = _accountroleRepository.GetByGuid(guid);
            if (accountrole is null)
            {
                return NotFound(response.NotFound("AccountRole Not Found"));
            }

            var data = _mapper.Map(accountrole);
            return Ok(response.Success(data, "AccountRole Found"));
        }

        [HttpPost]
        public IActionResult Create(AccountRoleVM accountroleVM)
        {
            var response = new ResponseVM<AccountRoleVM>();
            var accountroleConverted = _mapper.Map(accountroleVM);
            var result = _accountroleRepository.Create(accountroleConverted);
            if (result is null)
            {
                return BadRequest(response.Failed("Create Account Role Failed"));
            }

            return Ok(response.Success("Create Account Role Success"));
        }

        [HttpPut]
        public IActionResult Update(AccountRoleVM accountroleVM)
        {
            var response = new ResponseVM<AccountRoleVM>();
            var accountroleConverted = _mapper.Map(accountroleVM);
            var isUpdated = _accountroleRepository.Update(accountroleConverted);
            if (!isUpdated)
            {
                return BadRequest(response.Failed("Create AccountRole Failed"));
            }

            return Ok(response.Success("Update AccountRole Success"));
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var response = new ResponseVM<AccountRoleVM>();
            var isDeleted = _accountroleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(response.Failed("Delete AccountRole Failed"));
            }

            return Ok(response.Success("Delete AccountRole Success"));
        }
    }
}
