using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("FeedbackCategory")]
    public partial class FeedbackCategory
    {
        [Key]
        [Column("FeedbackCategory_ID")]
        public int FeedbackCategoryId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? FeedbackCategoryName { get; set; }

        /*
        [InverseProperty("FeedbackCategory")]
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        */
    }
}
