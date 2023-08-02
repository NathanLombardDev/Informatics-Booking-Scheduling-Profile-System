using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Service")]
    public partial class Service
    {
        [Key]
        [Column("Service_ID")]
        public int ServiceId { get; set; }

        [Column("BicyclePart_ID")]
        public int? BicyclePartId { get; set; }

        [Column("Bicycle_ID")]
        public int? BicycleId { get; set; }

        [StringLength(250)]
        [Unicode(false)]
        public string? ServiceDescription { get; set; }

        [ForeignKey("BicycleId")]
        //[InverseProperty("Services")]
        public virtual Bicycle? Bicycle { get; set; }

        [ForeignKey("BicyclePartId")]
        //[InverseProperty("Services")]
        public virtual BicyclePart? BicyclePart { get; set; }

        /*
        [InverseProperty("Service")]
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        */
    }
}
