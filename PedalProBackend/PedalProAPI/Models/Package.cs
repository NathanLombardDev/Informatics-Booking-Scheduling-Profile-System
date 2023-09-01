using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Package")]
    public partial class Package
    {
        [Key]
        [Column("Package_ID")]
        public int PackageId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? PackageName { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? PackageDescription { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public int? NumPackageBookings { get; set; }


        /*
        [InverseProperty("Package")]
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

        [InverseProperty("Package")]
        public virtual ICollection<ClientPackage> ClientPackages { get; set; } = new List<ClientPackage>();

        [InverseProperty("Package")]
        public virtual ICollection<PackagePrice> PackagePrices { get; set; } = new List<PackagePrice>();

        [InverseProperty("Package")]
        public virtual ICollection<TrainingModule> TrainingModules { get; set; } = new List<TrainingModule>();
        */
    }
}
