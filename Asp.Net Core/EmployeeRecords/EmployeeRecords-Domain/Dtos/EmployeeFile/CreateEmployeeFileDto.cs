using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecords_Domain.Dtos.EmployeeFile
{
    public class CreateEmployeeFileDto
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
