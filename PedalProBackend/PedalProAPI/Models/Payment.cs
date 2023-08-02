using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Payment")]
    public partial class Payment
    {
        [Key]
        [Column("Payment_ID")]
        public int PaymentId { get; set; }

        public double? PaymentAmount { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? PaymentDate { get; set; }

        /*
        [InverseProperty("Payment")]
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

        [InverseProperty("Payment")]
        public virtual ICollection<Checkout> Checkouts { get; set; } = new List<Checkout>();
        */
    }
}
