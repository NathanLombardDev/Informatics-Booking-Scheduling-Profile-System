using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("ClientPackage")]
    public partial class ClientPackage
    {
        [Key]
        [Column("ClientPackage")]
        public int ClientPackage1 { get; set; }

        [Column("Package_ID")]
        public int? PackageId { get; set; }

        [Column("ClientID")]
        public int? ClientId { get; set; }

        [ForeignKey("ClientId")]
        //[InverseProperty("ClientPackages")]
        public virtual Client? Client { get; set; }

        [ForeignKey("PackageId")]
        //[InverseProperty("ClientPackages")]
        public virtual Package? Package { get; set; }
    }
}
