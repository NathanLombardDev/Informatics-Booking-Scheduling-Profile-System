using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("BookingStatus")]
    public partial class BookingStatus
    {
        [Key]
        [Column("BookingStatus_ID")]
        public int BookingStatusId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? BookingStatusName { get; set; }

        /*
        [InverseProperty("BookingStatus")]
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        */
    }
}
