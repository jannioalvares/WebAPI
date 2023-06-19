using Client.Repositories.Interface;
using Client.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository repository;

        public AccountController(IAccountRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Logins()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logins(LoginVM loginVM)
        {
            var result = await repository.Logins(loginVM);
            if (result is null)
            {
                return RedirectToAction("Error", "Home");
            }
            else if (result.Code == 400)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            else if (result.Code == 404)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            else if (result.Code == 200)
            {
                HttpContext.Session.SetString("JWToken", result.Data);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Logins", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registers()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registers(RegisterVM registerVM)
        {

            var result = await repository.Registers(registerVM);
            if (result is null)
            {
                return RedirectToAction("Error", "Home");
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                TempData["Error"] = $"Something Went Wrong! - {result.Message}!";
                return View();
            }
            else if (result.Code == 200)
            {
                TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
                return RedirectToAction("Logins", "Account");
            }
            return View();
        }

    }
}
