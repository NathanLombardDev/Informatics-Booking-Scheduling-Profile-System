using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Client")]

    public partial class Client
    {
        [Key]
        [Column("ClientID")]
        public int ClientId { get; set; }

        [Column("UserID")]
        [ForeignKey("PedalProUser")]
        public string UserId { get; set; }

        [Column("ClientType_ID")]
        public int? ClientTypeId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? ClientTitle { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? ClientName { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? ClientSurname { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? ClientEmailAddress { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? ClientPhysicalAddress { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? ClientPhoneNum { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ClientDateOfBirth { get; set; }

        [Unicode(false)]
        public string? ClientProfilePicture { get; set; }

        /*
        [InverseProperty("Client")]
        public virtual ICollection<Bicycle> Bicycles { get; set; } = new List<Bicycle>();

        [InverseProperty("Client")]
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        [InverseProperty("Client")]
        public virtual ICollection<ClientPackage> ClientPackages { get; set; } = new List<ClientPackage>();
        */

        [ForeignKey("ClientTypeId")]
        //[InverseProperty("Clients")]
        public virtual ClientType? ClientType { get; set; }

        /*
        [InverseProperty("Client")]
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

        [InverseProperty("Client")]
        public virtual ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();
        */

        [ForeignKey("UserId")]
        //[InverseProperty("Clients")]
        public virtual PedalProUser? User { get; set; }

        /*
        [InverseProperty("Client")]
        public virtual ICollection<Workout> Workouts { get; set; } = new List<Workout>();
        */
    }
}
