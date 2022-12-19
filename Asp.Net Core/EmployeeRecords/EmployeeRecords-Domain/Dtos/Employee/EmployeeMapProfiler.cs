using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecords_Domain.Dtos.Employee
{
    public class EmployeeMapProfiler : Profile
    {
        public EmployeeMapProfiler()
        {
            CreateMap<EmployeeRecords_Domain.Models.Employee, EmployeeDto>()
                .ForMember(x => x.DepartmentName,opt => opt.MapFrom(x => x.Department.Name));
            CreateMap<CreateEmployeeDto, EmployeeRecords_Domain.Models.Employee>();
            CreateMap<UpdateEmployeeDto, EmployeeRecords_Domain.Models.Employee>()
                .ForMember(x => x.EmployeeFiles, opt => opt.MapFrom(x => x.DeletedEmployeeFiles));
        }
    }
}
