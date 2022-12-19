using AutoMapper;
using EmployeeRecords_Domain.Dtos.Department;
using EmployeeRecords_Domain.Dtos.EmployeeFile;
using EmployeeRecords_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecords_Domain.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department,DepartmentDto>();
            CreateMap<EmployeeFile, EmployeeFileDto>();
        }
    }
}
