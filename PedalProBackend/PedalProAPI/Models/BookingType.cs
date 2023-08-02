using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("BookingType")]
    public partial class BookingType
    {
        [Key]
        [Column("BookingType_ID")]
        public int BookingTypeId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? BookingTypeName { get; set; }

        /*
        [InverseProperty("BookingType")]
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        */
    }
}
