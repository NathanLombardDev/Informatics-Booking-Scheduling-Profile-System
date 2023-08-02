using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("EmployeeType")]
    public partial class EmployeeType
    {
        [Key]
        [Column("EmpType_ID")]
        public int EmpTypeId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? EmpTypeName { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? EmpTypeDescription { get; set; }

        /*
        [InverseProperty("EmpType")]
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
        */
    }
}
