using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Administrator")]
    public partial class Administrator
    {
        [Key]
        [Column("AdministratorID")]
        public int AdministratorId { get; set; }

        [Column("UserID")]
        public string UserId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? Title { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? AdminName { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? AdminSurname { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? AdminEmail { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? AdminPhoneNum { get; set; }

        [ForeignKey("UserId")]
        //[InverseProperty("Administrators")]
        public virtual PedalProUser? User { get; set; }
    }
}
