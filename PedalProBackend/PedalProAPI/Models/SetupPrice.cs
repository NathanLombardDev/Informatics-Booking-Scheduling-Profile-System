using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("SetupPrice")]
    public partial class SetupPrice
    {
        [Key]
        [Column("SetupPrice_ID")]
        public int SetupPriceId { get; set; }

        [Column("Setup_ID")]
        public int? SetupId { get; set; }

        [Column("Price_ID")]
        public int? PriceId { get; set; }

        [ForeignKey("PriceId")]
        //[InverseProperty("SetupPrices")]
        public virtual Price? Price { get; set; }

        [ForeignKey("SetupId")]
        //[InverseProperty("SetupPrices")]
        public virtual Setup? Setup { get; set; }
    }
}
