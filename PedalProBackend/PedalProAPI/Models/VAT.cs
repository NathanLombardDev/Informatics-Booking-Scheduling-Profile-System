using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedalProAPI.Models
{
    [Table("VAT")]
    public partial class VAT
    {
        [Key]
        [Column("VAT_ID")]
        public int VatId { get; set; }

        [Column("VATPecerntage")]
        public double? Vatpecerntage { get; set; }

        [Column("VATDate", TypeName = "date")]
        public DateTime? Vatdate { get; set; }
    }
}
