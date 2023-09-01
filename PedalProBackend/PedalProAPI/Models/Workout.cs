using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Workout")]
    public partial class Workout
    {
        [Key]
        [Column("Workout_ID")]
        public int WorkoutId { get; set; }

        public double? Distance { get; set; }

        public TimeSpan? Duration { get; set; }

        public int? HeartRate { get; set; }

        [Column("Client_ID")]
        public int? ClientId { get; set; }

        [Column("WorkoutType_ID")]
        public int? WorkoutTypeId { get; set; }

        public DateTime? WorkoutDate { get; set; }

        [ForeignKey("ClientId")]
        //[InverseProperty("Workouts")]
        public virtual Client? Client { get; set; }

        [ForeignKey("WorkoutTypeId")]
        //[InverseProperty("Workouts")]
        public virtual WorkoutType? WorkoutType { get; set; }
    }
}
