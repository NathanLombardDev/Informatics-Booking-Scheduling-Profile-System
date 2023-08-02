using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Bicycle")]
    public partial class Bicycle
    {
        [Key]
        [Column("Bicycle_ID")]
        public int BicycleId { get; set; }

        [Column("ClientID")]
        public int? ClientId { get; set; }

        [Column("BicycleCategory_ID")]
        public int? BicycleCategoryId { get; set; }

        [Column("BicycleBrand_ID")]
        public int? BicycleBrandId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? BicycleName { get; set; }

        [ForeignKey("BicycleBrandId")]
        //[InverseProperty("Bicycles")]
        public virtual BicycleBrand? BicycleBrand { get; set; }

        [ForeignKey("BicycleCategoryId")]
        //InverseProperty("Bicycles")]
        public virtual BicycleCategory? BicycleCategory { get; set; }

        [ForeignKey("ClientId")]
        //[InverseProperty("Bicycles")]
        public virtual Client? Client { get; set; }

        /*
        [InverseProperty("Bicycle")]
        public virtual ICollection<Service> Services { get; set; } = new List<Service>();

        [InverseProperty("Bicycle")]
        public virtual ICollection<Setup> Setups { get; set; } = new List<Setup>();
        */
    }
}
