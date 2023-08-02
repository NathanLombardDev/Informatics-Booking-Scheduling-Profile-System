using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("RefundReason")]
    public partial class RefundReason
    {
        [Key]
        [Column("RefundReason_ID")]
        public int RefundReasonId { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? RefundReasonDesc { get; set; }

        /*
        [InverseProperty("RefundReason")]
        public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();
        */
    }
}
