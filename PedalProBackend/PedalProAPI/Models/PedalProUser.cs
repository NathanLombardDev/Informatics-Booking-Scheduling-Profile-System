using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedalProAPI.Models
{
    [Table("AspNetUsers")]
    public class PedalProUser:IdentityUser
    {
    }
}
