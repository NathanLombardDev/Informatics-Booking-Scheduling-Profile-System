using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("BicycleBrand")]
    public partial class BicycleBrand
    {
        [Key]
        [Column("BicycleBrand_ID")]
        public int BicycleBrandId { get; set; }

        [Column("BrandImage_ID")]
        public int? BrandImageId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? BrandName { get; set; }

        /*
        [InverseProperty("BicycleBrand")]
        public virtual ICollection<Bicycle> Bicycles { get; set; } = new List<Bicycle>();
        */

        [ForeignKey("BrandImageId")]
        //[InverseProperty("BicycleBrands")]
        public virtual BrandImage? BrandImage { get; set; }
    }
}
