using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("TrainingMaterial")]
    public partial class TrainingMaterial
    {
        [Key]
        [Column("TrainingMaterial_ID")]
        public int TrainingMaterialId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? TrainingMaterialName { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? Content { get; set; }

        [Column("VideoLink_ID")]
        public int? VideoLinkId { get; set; }

        [Column("TrainingModule_ID")]
        public int? TrainingModuleId { get; set; }

        [ForeignKey("TrainingModuleId")]
        //[InverseProperty("TrainingMaterials")]
        public virtual TrainingModule? TrainingModule { get; set; }

        [ForeignKey("VideoLinkId")]
        //[InverseProperty("TrainingMaterials")]
        public virtual VideoLink? VideoLink { get; set; }

        internal bool Any()
        {
            throw new NotImplementedException();
        }
    }
}
