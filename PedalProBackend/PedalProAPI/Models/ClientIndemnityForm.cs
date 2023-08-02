using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    public class ClientIndemnityForm
    {
        [Key]
        [Column("IndemnityForm_ID")]
        public int ClientIndemnityFormId { get; set; }
        public string Title { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime DateUploaded { get; set; }

        [Column("Client_ID")]
        public int? ClientId { get; set; }


        [ForeignKey("ClientId")]
        //[InverseProperty("Workouts")]
        public virtual Client? Client { get; set; }
    }
}
