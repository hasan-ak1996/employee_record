using EmployeeRecords_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecords_Repository.IRepository
{
    public interface IEmployeeFileRepository : IGenericRepository<EmployeeFile>
    {
        public Task<bool> AddListAsync(List<EmployeeFile> files);
    }
}
