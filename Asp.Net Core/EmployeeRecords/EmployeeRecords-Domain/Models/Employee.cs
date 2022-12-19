using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecords_Domain.Models
{
    public class Employee : BaseEntity
    {
        public Employee()
        {
            EmployeeFiles = new List<EmployeeFile>();
        }
        [Required]
        public string Name { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public virtual ICollection<EmployeeFile> EmployeeFiles { get; set; }

    }
}
