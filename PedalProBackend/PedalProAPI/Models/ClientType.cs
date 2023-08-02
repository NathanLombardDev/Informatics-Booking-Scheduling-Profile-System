using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("ClientType")]
    public partial class ClientType
    {
        [Key]
        [Column("ClientType_ID")]
        public int ClientTypeId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? ClientTypeName { get; set; }

        /*
        [InverseProperty("ClientType")]
        public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
        */
    }
}
