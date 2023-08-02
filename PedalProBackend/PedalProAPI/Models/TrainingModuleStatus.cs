using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("TrainingModuleStatus")]
    public partial class TrainingModuleStatus
    {
        [Key]
        [Column("TrainingModuleStatus_ID")]
        public int TrainingModuleStatusId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? TrainingModuleStatusName { get; set; }


        /*
        [InverseProperty("TrainingModuleStatus")]
        public virtual ICollection<TrainingModule> TrainingModules { get; set; } = new List<TrainingModule>();
        */
    }
}
