using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("CartStatus")]
    public partial class CartStatus
    {
        [Key]
        [Column("CartStatus_ID")]
        public int CartStatusId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? CartStatusName { get; set; }

        /*
        [InverseProperty("CartStatus")]
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
        */
    }
}
