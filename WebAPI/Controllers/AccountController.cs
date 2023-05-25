using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Repositories;
using WebAPI.ViewModels.Accounts;
using WebAPI.ViewModels.Login;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper<Account, AccountVM> _mapper;
        public AccountController(IAccountRepository accountRepository, IMapper<Account, AccountVM> mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accounts = _accountRepository.GetAll();
            if (!accounts.Any())
            {
                return NotFound();
            }

            var data = accounts.Select(_mapper.Map).ToList();
            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if (account is null)
            {
                return NotFound();
            }

            var data = _mapper.Map(account);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(AccountVM accountVM)
        {
            var accountConverted = _mapper.Map(accountVM);
            var result = _accountRepository.Create(accountConverted);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(AccountVM accountVM)
        {
            var accountConverted = _mapper.Map(accountVM);
            var isUpdated = _accountRepository.Update(accountConverted);
            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _accountRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginVM loginVM)
        {
            var account = _accountRepository.Login(loginVM);

            if (account == null)
            {
                return NotFound("Account not found");
            }

            if (account.Password != loginVM.Password)
            {
                return BadRequest("Password is invalid");
            }

            return Ok();

        }
    }
}
