using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("BicyclePart")]
    public partial class BicyclePart
    {
        [Key]
        [Column("BicyclePart_ID")]
        public int BicyclePartId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? BicyclePartName { get; set; }

        /*
        [InverseProperty("BicyclePart")]
        public virtual ICollection<Service> Services { get; set; } = new List<Service>();
        */
    }
}
