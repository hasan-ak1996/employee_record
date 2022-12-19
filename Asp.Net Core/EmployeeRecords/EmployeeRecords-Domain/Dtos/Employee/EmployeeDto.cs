using EmployeeRecords_Domain.Dtos.EmployeeFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecords_Domain.Dtos.Employee
{
    public class EmployeeDto : BaseEntityDto
    {
        public string Name { get; set; }
        public int? DepartmentId { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreationTime { get; set; }
        public List<EmployeeFileDto> EmployeeFiles { get; set; }

    }
}
