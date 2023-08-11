using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Cart")]
    public partial class Cart
    {
        [Key]
        [Column("Cart_ID")]
        public int CartId { get; set; }

        public int? CartQuantity { get; set; }

        public double? CartAmount { get; set; }

        [Column("CartStatus_ID")]
        public int? CartStatusId { get; set; }

        /*[Column("Payment_ID")]
        public int? PaymentId { get; set; }*/

        [Column("Package_ID")]
        public int? PackageId { get; set; }

        [ForeignKey("CartStatusId")]
        //[InverseProperty("Carts")]
        public virtual CartStatus? CartStatus { get; set; }

        /*
        [InverseProperty("Cart")]
        public virtual ICollection<Checkout> Checkouts { get; set; } = new List<Checkout>();
        */

        [ForeignKey("PackageId")]
        //[InverseProperty("Carts")]
        public virtual Package? Package { get; set; }

        public virtual ICollection<Package> Packages { get; set; } = new List<Package>(); // One-to-Many relationship

        /*([ForeignKey("PaymentId")]
        //[InverseProperty("Carts")]
        public virtual Payment? Payment { get; set; }*/
    }
}
