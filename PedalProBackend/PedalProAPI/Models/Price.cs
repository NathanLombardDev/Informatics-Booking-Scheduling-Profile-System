using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Price")]
    public partial class Price
    {
        [Key]
        [Column("Price_ID")]
        public int PriceId { get; set; }

        [Column("Price")]
        public double? Price1 { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? PriceDate { get; set; }

        /*
        [InverseProperty("Price")]
        public virtual ICollection<PackagePrice> PackagePrices { get; set; } = new List<PackagePrice>();

        [InverseProperty("Price")]
        public virtual ICollection<ServicePrice> ServicePrices { get; set; } = new List<ServicePrice>();

        [InverseProperty("Price")]
        public virtual ICollection<SetupPrice> SetupPrices { get; set; } = new List<SetupPrice>();
        */
    }
}
