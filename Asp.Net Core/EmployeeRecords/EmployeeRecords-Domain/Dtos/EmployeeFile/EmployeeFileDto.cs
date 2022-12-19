using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecords_Domain.Dtos.EmployeeFile
{
    public class EmployeeFileDto : BaseEntityDto
    {
        public string FileName { get; set; }
        public decimal FileSize { get; set; }
        public string FileUrl { get; set; }
        public int EmployeeId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
