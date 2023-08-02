using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PedalProAPI.Models
{
    [Table("Employee")]
    public partial class Employee
    {
        [Key]
        [Column("EmployeeID")]
        public int EmployeeId { get; set; }

        [Column("UserID")]
        public string UserId { get; set; }

        [Column("EmpType_ID")]
        public int? EmpTypeId { get; set; }

        [Column("EmpStatus_ID")]
        public int? EmpStatusId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? EmpTitle { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? EmpName { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? EmpSurname { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? EmpPhoneNum { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? EmpEmailAddress { get; set; }

        [ForeignKey("EmpStatusId")]
        //[InverseProperty("Employees")]
        public virtual EmployeeStatus? EmpStatus { get; set; }

        [ForeignKey("EmpTypeId")]
        //[InverseProperty("Employees")]
        public virtual EmployeeType? EmpType { get; set; }

        /*
        [InverseProperty("Employee")]
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        */

        [ForeignKey("UserId")]
        //[InverseProperty("Employees")]
        public virtual PedalProUser? User { get; set; }
    }
}
