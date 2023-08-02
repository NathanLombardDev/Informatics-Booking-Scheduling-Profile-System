using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Refund")]
    public partial class Refund
    {
        [Key]
        [Column("Refund_ID")]
        public int RefundId { get; set; }

        [Column("Booking_ID")]
        public int? BookingId { get; set; }

        [Column("RefundReason_ID")]
        public int? RefundReasonId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? RefundDate { get; set; }

        public double? RefundAmount { get; set; }

        [ForeignKey("BookingId")]
        //[InverseProperty("Refunds")]
        public virtual Booking? Booking { get; set; }

        [ForeignKey("RefundReasonId")]
        //[InverseProperty("Refunds")]
        public virtual RefundReason? RefundReason { get; set; }
    }
}
