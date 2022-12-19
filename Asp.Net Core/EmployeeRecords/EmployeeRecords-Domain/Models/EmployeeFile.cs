using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecords_Domain.Models
{
    public class EmployeeFile : BaseEntity
    {
        public string FileName { get; set; }
        public decimal FileSize { get; set; }
        public string FileUrl { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
