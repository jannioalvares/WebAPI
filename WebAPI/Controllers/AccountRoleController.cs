using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : ControllerBase
    {

        private readonly IGenericRepository<AccountRole> _accountRoleRepository;
        public AccountRoleController(IGenericRepository<AccountRole> accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accountroles = _accountRoleRepository.GetAll();
            if (!accountroles.Any())
            {
                return NotFound();
            }

            return Ok(accountroles);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var accountrole = _accountRoleRepository.GetByGuid(guid);
            if (accountrole is null)
            {
                return NotFound();
            }

            return Ok(accountrole);
        }

        [HttpPost]
        public IActionResult Create(AccountRole accountrole)
        {
            var result = _accountRoleRepository.Create(accountrole);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(AccountRole accountrole)
        {
            var isUpdated = _accountRoleRepository.Update(accountrole);
            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _accountRoleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
