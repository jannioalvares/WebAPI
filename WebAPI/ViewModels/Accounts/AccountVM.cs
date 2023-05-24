using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.ViewModels.Accounts
{
    public class AccountVM
    {
        public Guid? Guid { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public int Otp { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
