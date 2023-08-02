using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Schedule")]
    public partial class Schedule
    {
        [Key]
        [Column("Schedule_ID")]
        public int ScheduleId { get; set; }

        [Column("Dateslot_ID")]
        public int? DateslotId { get; set; }

        [Column("Setup_ID")]
        public int? SetupId { get; set; }

        [Column("Employee_ID")]
        public int? EmployeeId { get; set; }

        [Column("TrainingSession_ID")]
        public int? TrainingSessionId { get; set; }

        [Column("Service_ID")]
        public int? ServiceId { get; set; }

        /*
        [InverseProperty("Schedule")]
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        */

        [ForeignKey("DateslotId")]
        //[InverseProperty("Schedules")]
        public virtual DateSlot? Dateslot { get; set; }

        [ForeignKey("EmployeeId")]
        // [InverseProperty("Schedules")]
        public virtual Employee? Employee { get; set; }

        [ForeignKey("ServiceId")]
        //[InverseProperty("Schedules")]
        public virtual Service? Service { get; set; }

        [ForeignKey("SetupId")]
        //[InverseProperty("Schedules")]
        public virtual Setup? Setup { get; set; }

        [ForeignKey("TrainingSessionId")]
        //[InverseProperty("Schedules")]
        public virtual TrainingSession? TrainingSession { get; set; }
    }
}
