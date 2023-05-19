using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleRepository _accountroleRepository;
        public AccountRoleController(IAccountRoleRepository accountroleRepository)
        {
            _accountroleRepository = accountroleRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accountroles = _accountroleRepository.GetAll();
            if (!accountroles.Any())
            {
                return NotFound();
            }

            return Ok(accountroles);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var accountrole = _accountroleRepository.GetByGuid(guid);
            if (accountrole is null)
            {
                return NotFound();
            }

            return Ok(accountrole);
        }

        [HttpPost]
        public IActionResult Create(AccountRole accountrole)
        {
            var result = _accountroleRepository.Create(accountrole);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(AccountRole accountrole)
        {
            var isUpdated = _accountroleRepository.Update(accountrole);
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
