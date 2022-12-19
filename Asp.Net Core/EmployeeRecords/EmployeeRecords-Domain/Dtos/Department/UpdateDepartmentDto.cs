using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecords_Domain.Dtos.Department
{
    public class UpdateDepartmentDto : BaseEntityDto
    {
        [Required]
        public string Name { get; set; }
        
    }
}
