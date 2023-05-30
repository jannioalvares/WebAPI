using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.Accounts
{
    public class LoginVM
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
