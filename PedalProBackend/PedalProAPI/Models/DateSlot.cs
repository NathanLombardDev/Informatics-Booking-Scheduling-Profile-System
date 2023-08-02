using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("DateSlot")]
    public partial class DateSlot
    {
        [Key]
        [Column("DateSlot_ID")]
        public int DateSlotId { get; set; }

        [Column("Timeslot_ID")]
        public int? TimeslotId { get; set; }

        [Column("Date_ID")]
        public int? DateId { get; set; }

        [ForeignKey("DateId")]
        //[InverseProperty("DateSlots")]
        public virtual Date? Date { get; set; }

        /*
        [InverseProperty("Dateslot")]
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        */

        [ForeignKey("TimeslotId")]
        //[InverseProperty("DateSlots")]
        public virtual Timeslot? Timeslot { get; set; }
    }
}
