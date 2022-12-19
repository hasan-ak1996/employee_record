using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecords_Domain.Dtos.EmployeeFile
{
    public class EmployeeFileMapProfiler : Profile
    {
        public EmployeeFileMapProfiler()
        {
            CreateMap<CreateEmployeeFileDto, EmployeeRecords_Domain.Models.EmployeeFile>();
            CreateMap<EmployeeFileDto, EmployeeRecords_Domain.Models.EmployeeFile>();
        }
    }
}
