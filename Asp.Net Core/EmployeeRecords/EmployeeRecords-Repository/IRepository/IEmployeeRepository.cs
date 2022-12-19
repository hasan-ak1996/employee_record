using EmployeeRecords_Domain.Dtos;
using EmployeeRecords_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecords_Repository.IRepository
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public Task<Employee> GetByIdWithNavigartionProperty(int id);
        public Task<int> AddWithGetIdAsync(Employee entity);
        public Task<int> GetTotalCountAsync(PagedResultRequestDto input);
    }
}
