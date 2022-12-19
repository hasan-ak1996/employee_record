using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecords_Domain.Dtos
{
    public class PagedResultRequestDto
    {
        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }
        [Range(1, int.MaxValue)]
        public int MaxResult { get; set; } = 10;
        public string Keyword { get; set; }
    }
}
