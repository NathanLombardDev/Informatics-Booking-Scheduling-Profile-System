using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Feedback")]
    public partial class Feedback
    {
        [Key]
        [Column("Feedback_ID")]
        public int FeedbackId { get; set; }

        [Unicode(false)]
        public string? FeedbackDescription { get; set; }

        public int? FeedbackRating { get; set; }

        [Column("FeedbackCategory_ID")]
        public int? FeedbackCategoryId { get; set; }

        [Column("Client_ID")]
        public int? ClientId { get; set; }

        [Column("Trainingsession_ID")]
        public int? TrainingsessionId { get; set; }

        [ForeignKey("ClientId")]
        //[InverseProperty("Feedbacks")]
        public virtual Client? Client { get; set; }

        [ForeignKey("FeedbackCategoryId")]
        //[InverseProperty("Feedbacks")]
        public virtual FeedbackCategory? FeedbackCategory { get; set; }

        [ForeignKey("TrainingsessionId")]
        //[InverseProperty("Feedbacks")]
        public virtual TrainingSession? Trainingsession { get; set; }
    }
}
