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

        /*
        [InverseProperty("Timeslot")]
        public virtual ICollection<DateSlot> DateSlots { get; set; } = new List<DateSlot>();
        */
    }
}
