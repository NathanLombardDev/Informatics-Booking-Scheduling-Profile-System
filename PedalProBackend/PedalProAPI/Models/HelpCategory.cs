using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("HELPCategory")]
    public partial class HelpCategory
    {
        [Key]
        [Column("HelpCategory_ID")]
        public int HelpCategoryId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? HelpCategoryName { get; set; }

        /*
        [InverseProperty("HelpCategory")]
        public virtual ICollection<Help> Helps { get; set; } = new List<Help>();
        */
    }
}
