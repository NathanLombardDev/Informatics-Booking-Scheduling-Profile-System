using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Setup")]
    public partial class Setup
    {
        [Key]
        [Column("Setup_ID")]
        public int SetupId { get; set; }

        [Column("Bicycle_ID")]
        public int? BicycleId { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? SetupDescription { get; set; }

        [ForeignKey("BicycleId")]
        //[InverseProperty("Setups")]
        public virtual Bicycle? Bicycle { get; set; }

        /*
        [InverseProperty("Setup")]
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

        [InverseProperty("Setup")]
        public virtual ICollection<ServicePrice> ServicePrices { get; set; } = new List<ServicePrice>();

        [InverseProperty("Setup")]
        public virtual ICollection<SetupPrice> SetupPrices { get; set; } = new List<SetupPrice>();
        */
    }
}
