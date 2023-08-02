using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Booking")]
    public partial class Booking
    {
        [Key]
        [Column("Booking_ID")]
        public int BookingId { get; set; }

        [Column("BookingStatus_ID")]
        public int? BookingStatusId { get; set; }

        [Column("ClientID")]
        public int? ClientId { get; set; }

        [Column("BookingType_ID")]
        public int? BookingTypeId { get; set; }

        [Column("Schedule_ID")]
        public int? ScheduleId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? ReferenceNum { get; set; }

        [ForeignKey("BookingStatusId")]
        //[InverseProperty("Bookings")]
        public virtual BookingStatus? BookingStatus { get; set; }

        [ForeignKey("BookingTypeId")]
        // [InverseProperty("Bookings")]
        public virtual BookingType? BookingType { get; set; }

        [ForeignKey("ClientId")]
        // [InverseProperty("Bookings")]
        public virtual Client? Client { get; set; }

        /*
        [InverseProperty("Booking")]
        public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();
        */

        [ForeignKey("ScheduleId")]
        //[InverseProperty("Bookings")]
        public virtual Schedule? Schedule { get; set; }
    }
}
