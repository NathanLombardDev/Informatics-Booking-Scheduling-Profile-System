using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("PedalProRole")]
    public partial class PedalProRole
    {
        [Key]
        [Column("Role_ID")]
        public int RoleId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? RoleName { get; set; }


    }
}
