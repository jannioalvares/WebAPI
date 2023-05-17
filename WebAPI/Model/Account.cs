using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Model
{
    [Table("tb_m_accounts")]
    public class Account : BaseEntity
    {
        [Column("password")]
        public string Password { get; set; }
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        [Column("otp")]
        public int Otp { get; set; }
        [Column("is_used")]
        public bool IsUsed { get; set; }
        [Column("expired_time")]
        public DateTime ExpiredTime { get; set; }

        public Employee Employee { get; set; }
        public ICollection<AccountRole> AccountRoles { get; set; }
    }
}
