using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("VideoType")]
    public partial class VideoType
    {
        [Key]
        [Column("VideoType_ID")]
        public int VideoTypeId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? VideoTypeName { get; set; }

        /*
        [InverseProperty("VideoType")]
        public virtual ICollection<VideoLink> VideoLinks { get; set; } = new List<VideoLink>();
        */
    }
}
