using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("CheckoutStatus")]
    public partial class CheckoutStatus
    {
        [Key]
        [Column("CheckoutStatus_ID")]
        public int CheckoutStatusId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? CheckoutStatusName { get; set; }

        /*
        [InverseProperty("CheckoutStatus")]
        public virtual ICollection<Checkout> Checkouts { get; set; } = new List<Checkout>();
        */
    }
}
