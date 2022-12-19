using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecords_Domain.Dtos.Department
{
    public class DepartmentMapProfiler : Profile
    {
        public DepartmentMapProfiler()
        {
            CreateMap<EmployeeRecords_Domain.Models.Department, DepartmentDto>();
            CreateMap<CreateDepartmentDto,EmployeeRecords_Domain.Models.Department>();
            CreateMap<UpdateDepartmentDto,EmployeeRecords_Domain.Models.Department>();
        }
    }
}
