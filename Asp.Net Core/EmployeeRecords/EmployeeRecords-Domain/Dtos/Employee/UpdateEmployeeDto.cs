using EmployeeRecords_Domain.Dtos.EmployeeFile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecords_Domain.Dtos.Employee
{
    public class UpdateEmployeeDto : BaseEntityDto
    {
        [Required]
        public string Name { get; set; }
        public int? DepartmentId { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public List<EmployeeFileDto> DeletedEmployeeFiles { get; set; }
    }
}
