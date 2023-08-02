using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("VideoLink")]
    public partial class VideoLink
    {
        [Key]
        [Column("VideoLink_ID")]
        public int VideoLinkId { get; set; }

        [Column("VideoURL")]
        [StringLength(550)]
        [Unicode(false)]
        public string? VideoUrl { get; set; }

        [Column("VideoType_ID")]
        public int? VideoTypeId { get; set; }
    
        /*
        [InverseProperty("VideoLink")]
        public virtual ICollection<TrainingMaterial> TrainingMaterials { get; set; } = new List<TrainingMaterial>();
        */

        [ForeignKey("VideoTypeId")]
        //[InverseProperty("VideoLinks")]
        public virtual VideoType? VideoType { get; set; }
    }
}
