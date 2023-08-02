using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("HELP")]
    public partial class Help
    {
        [Key]
        [Column("Help_ID")]
        public int HelpId { get; set; }

        [Column("HelpCategory_ID")]
        public int? HelpCategoryId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? HelpName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? HelpDescription { get; set; }

        [ForeignKey("HelpCategoryId")]
        //[InverseProperty("Helps")]
        public virtual HelpCategory? HelpCategory { get; set; }
    }
}
