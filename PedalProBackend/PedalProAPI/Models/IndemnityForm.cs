using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PedalProAPI.Models
{
    public partial class IndemnityForm
    {

        [Key]
        [Column("IndemnityForm_ID")]
        public int IndemnityFormId { get; set; }
        public string Title { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime DateUploaded { get; set; }
        
    }
}
