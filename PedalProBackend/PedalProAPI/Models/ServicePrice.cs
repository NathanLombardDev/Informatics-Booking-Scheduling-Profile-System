using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("ServicePrice")]
    public partial class ServicePrice
    {
        [Key]
        [Column("ServicePrice_ID")]
        public int ServicePriceId { get; set; }

        [Column("Setup_ID")]
        public int? SetupId { get; set; }

        [Column("Price_ID")]
        public int? PriceId { get; set; }

        [ForeignKey("PriceId")]
        //[InverseProperty("ServicePrices")]
        public virtual Price? Price { get; set; }

        [ForeignKey("SetupId")]
        //[InverseProperty("ServicePrices")]
        public virtual Setup? Setup { get; set; }
    }
}
