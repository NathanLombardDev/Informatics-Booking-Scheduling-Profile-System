using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("PackagePrice")]
    public partial class PackagePrice
    {
        [Key]
        [Column("PackagePrice_ID")]
        public int PackagePriceId { get; set; }

        [Column("Package_ID")]
        public int? PackageId { get; set; }

        [Column("Price_ID")]
        public int? PriceId { get; set; }

        [ForeignKey("PackageId")]
        //[InverseProperty("PackagePrices")]
        public virtual Package? Package { get; set; }

        [ForeignKey("PriceId")]
        //[InverseProperty("PackagePrices")]
        public virtual Price? Price { get; set; }
    }
}
