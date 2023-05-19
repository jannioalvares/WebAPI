using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Model
{
    [Table("tb_m_account_roles")]
    public class AccountRole : BaseEntity
    {
        [Column("account_guid")]
        public Guid AccountGuid { get; set; }
        [Column("role_guid")]
        public Guid RoleGuid { get; set; }
        
        //Cardinality
        public Account? Account { get; set; }
        public Role? Role { get; set; }
    }
}
