using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("BicycleCategory")]
    public partial class BicycleCategory
    {
        [Key]
        [Column("BicycleCategory_ID")]
        public int BicycleCategoryId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? BicycleCategoryName { get; set; }

        /*
        [InverseProperty("BicycleCategory")]
        public virtual ICollection<Bicycle> Bicycles { get; set; } = new List<Bicycle>();
        */
    }
}
