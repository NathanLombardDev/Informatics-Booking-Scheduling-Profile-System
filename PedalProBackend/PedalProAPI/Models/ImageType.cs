using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("ImageType")]
    public partial class ImageType
    {
        [Key]
        [Column("ImageType_ID")]
        public int ImageTypeId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? ImageTypeName { get; set; }

        /*
        [InverseProperty("ImageType")]
        public virtual ICollection<BrandImage> BrandImages { get; set; } = new List<BrandImage>();
        */
    }
}
