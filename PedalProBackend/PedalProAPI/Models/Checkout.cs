using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Checkout")]
    public partial class Checkout
    {
        [Key]
        [Column("Checkout_ID")]
        public int CheckoutId { get; set; }

        [Column("CheckoutStatus_ID")]
        public int? CheckoutStatusId { get; set; }

        [Column("Payment_ID")]
        public int? PaymentId { get; set; }

        [Column("Cart_ID")]
        public int? CartId { get; set; }

        [ForeignKey("CartId")]
        //[InverseProperty("Checkouts")]
        public virtual Cart? Cart { get; set; }

        [ForeignKey("CheckoutStatusId")]
        //[InverseProperty("Checkouts")]
        public virtual CheckoutStatus? CheckoutStatus { get; set; }

        [ForeignKey("PaymentId")]
        //[InverseProperty("Checkouts")]
        public virtual Payment? Payment { get; set; }
    }
}
