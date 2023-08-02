using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Date")]
    public partial class Date
    {
        [Key]
        [Column("Date_ID")]
        public int DateId { get; set; }

        [Column("Date", TypeName = "datetime")]
        public DateTime? Date1 { get; set; }

        /*
        [InverseProperty("Date")]
        public virtual ICollection<DateSlot> DateSlots { get; set; } = new List<DateSlot>();
        */
    }
}
