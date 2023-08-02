using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("BrandImage")]
    public partial class BrandImage
    {
        [Key]
        [Column("BrandImage_ID")]
        public int BrandImageId { get; set; }

        [Column("ImageType_ID")]
        public int? ImageTypeId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? BrandImgName { get; set; }

        [Column("ImageURL")]
        [StringLength(10)]
        [Unicode(false)]
        public string? ImageUrl { get; set; }

        /*
        [InverseProperty("BrandImage")]
        public virtual ICollection<BicycleBrand> BicycleBrands { get; set; } = new List<BicycleBrand>();
        */

        [ForeignKey("ImageTypeId")]
        //[InverseProperty("BrandImages")]
        public virtual ImageType? ImageType { get; set; }

        internal bool Any()
        {
            throw new NotImplementedException();
        }
    }
}
