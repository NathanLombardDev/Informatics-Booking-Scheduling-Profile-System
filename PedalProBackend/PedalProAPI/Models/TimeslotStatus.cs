using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("TimeslotStatus")]
    public partial class TimeslotStatus
    {
        [Key]
        [Column("TimeslotStatus_ID")]
        public int TimeslotStatusId { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? TimeslotStatusName { get; set; }
    }
}
