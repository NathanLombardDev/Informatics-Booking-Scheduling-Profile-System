using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Timeslot")]
    public partial class Timeslot
    {
        [Key]
        [Column("Timeslot_ID")]
        public int TimeslotId { get; set; }

        //[Column(TypeName = "datetime")]
        public string? StartTime { get; set; }

        //[Column(TypeName = "datetime")]
        public string? EndTime { get; set; }


        [Column("TimeslotStatus_ID")]
        public int? TimeslotStatusId { get; set; }

        /*
        [InverseProperty("TrainingModule")]
        public virtual ICollection<TrainingMaterial> TrainingMaterials { get; set; } = new List<TrainingMaterial>();
        */

        [ForeignKey("TrainingModuleStatusId")]
        //[InverseProperty("TrainingModules")]
        public virtual TimeslotStatus? TimeslotStatus { get; set; }

        /*
        [InverseProperty("Timeslot")]
        public virtual ICollection<DateSlot> DateSlots { get; set; } = new List<DateSlot>();
        */
    }
}
