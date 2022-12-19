using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecords_Domain.Dtos.EmployeeFile
{
    public class CreateListEmployeeFileDto
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public List<IFormFile> Files { get; set; }
    }
}
