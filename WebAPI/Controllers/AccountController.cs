using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Others;
using WebAPI.Repositories;
using WebAPI.Utility;
using WebAPI.ViewModels.Accounts;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController<Account, AccountVM>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper<Account, AccountVM> _mapper;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountRepository accountRepository,
            IMapper<Account, AccountVM> mapper,
            IEmployeeRepository employeeRepository,
            IEmailService emailService,
            ITokenService tokenService) : base(accountRepository, mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _emailService = emailService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(LoginVM loginVM)
        {
            var account = _accountRepository.Login(loginVM);
            var employee = _employeeRepository.GetByEmail(loginVM.Email);

            if (account == null)
            {
                return NotFound(new ResponseVM<LoginVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Account Not Found"
                });
            }

            if (account.Password != loginVM.Password)
            {
                return BadRequest(new ResponseVM<LoginVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Password Invalid"
                });
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, employee.Nik),
                new(ClaimTypes.Name, $"{employee.FirstName}{employee.LastName}"),
                new(ClaimTypes.Email, employee.Email),
            };

            var roles = _accountRepository.GetRoles(employee.Guid);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = _tokenService.GenerateToken(claims);

            return Ok(new ResponseVM<string>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Login Success",
                Data = token
            });

        }

        [Authorize]
        [HttpGet("GetDataFromToken")]
        public IActionResult GetDataFromToken(string token)
        {
            var data = _tokenService.ExtrackClaimsFromJwt(token);

            if (data.NameIdentifier == null)
            {
                return BadRequest(new ResponseVM<string>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Invalid Token"
                });
            }

            return Ok(new ResponseVM<ClaimVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = data
            });
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(RegisterVM registerVM)
        {
            var result = _accountRepository.Register(registerVM);
            switch (result)
            {
                case 0:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Registration failed"
                    });
                case 1:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Email already exists"
                    });
                case 2:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Phone number already exists"
                    });
                case 3:
                    return Ok(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Registration success"
                    });
            }

            return Ok(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Registration success"
            });

        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            // Cek apakah email dan OTP valid
            var response = new ResponseVM<ChangePasswordVM>();
            var account = _employeeRepository.FindGuidByEmail(changePasswordVM.Email);
            var changePass = _accountRepository.ChangePasswordAccount(account, changePasswordVM);
            switch (changePass)
            {
                case 0:
                    return BadRequest(response.Error("Runtime error"));
                case 1:
                    return Ok(response.Success("Password has been changed successfully"));
                case 2:
                    return BadRequest(response.Error("Invalid OTP"));
                case 3:
                    return BadRequest(response.Error("OTP has already been used"));
                case 4:
                    return BadRequest(response.Error("OTP expired"));
                case 5:
                    return BadRequest(response.Error("Password doesnt match"));
                default:
                    return BadRequest();
            }
            return null;

        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword/{email}")]
        public IActionResult UpdateResetPass(String email)
        {
            var response = new ResponseVM<AccountResetPasswordVM>();
            var getGuid = _employeeRepository.FindGuidByEmail(email);
            if (getGuid == null)
            {
                return NotFound(response.NotFound("Email not found"));
            }

            var isUpdated = _accountRepository.UpdateOTP(getGuid);

            switch (isUpdated)
            {
                case 0:
                    return BadRequest();
                default:
                    _emailService.SetEmail(email)
                        .SetSubject("Forgot Passowrd")
                        .SetHtmlMessage($"Your OTP is {isUpdated}")
                        .SendEmailAsync();

                    return Ok(response.Success("OTP Successfully Sent to Email"));

            }
        }
    }
}
