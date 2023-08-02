using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("TrainingSession")]
    public partial class TrainingSession
    {
        [Key]
        [Column("TrainingSession_ID")]
        public int TrainingSessionId { get; set; }

        [Column("ClientID")]
        public int? ClientId { get; set; }

        [StringLength(500)]
        [Unicode(false)]
        public string? TrainingSessionDescription { get; set; }

        [ForeignKey("ClientId")]
        //[InverseProperty("TrainingSessions")]
        public virtual Client? Client { get; set; }

        /*
        [InverseProperty("Trainingsession")]
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

        [InverseProperty("TrainingSession")]
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        */
    }
}
