using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("EmployeeStatus")]
    public partial class EmployeeStatus
    {
        [Key]
        [Column("EmpStatus_ID")]
        public int EmpStatusId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? EmpStatusName { get; set; }

        /*
        [InverseProperty("EmpStatus")]
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
        */
    }
}
