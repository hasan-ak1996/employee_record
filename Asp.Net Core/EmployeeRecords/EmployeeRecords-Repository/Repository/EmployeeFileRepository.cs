using EmployeeRecords_Domain.Dtos;
using EmployeeRecords_Domain.Dtos.Employee;
using EmployeeRecords_Domain.Models;
using EmployeeRecords_Repository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecords_Repository.Repository
{
    public class EmployeeFileRepository : IEmployeeFileRepository
    {
        private readonly IConfiguration _configuration;

        public EmployeeFileRepository(
            IConfiguration configuration
            )
        {
            _configuration = configuration;
        }

        public async Task<bool> AddAsync(EmployeeFile entity)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_create_employee_file", connection);
                cmd.Parameters.AddWithValue("FileName", entity.FileName);
                cmd.Parameters.AddWithValue("FileSize", entity.FileSize);
                cmd.Parameters.AddWithValue("FileUrl", entity.FileUrl);
                cmd.Parameters.AddWithValue("EmployeeId", entity.EmployeeId);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                int status = await cmd.ExecuteNonQueryAsync();
                if (status > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> AddListAsync(List<EmployeeFile> files)
        {

            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                SqlTransaction sqlTran = connection.BeginTransaction();
                try
                {
                    foreach (var file in files)
                    {
                        SqlCommand cmd = new SqlCommand("sp_create_employee_file", connection);
                        cmd.Transaction = sqlTran;
                        cmd.Parameters.AddWithValue("FileName", file.FileName);
                        cmd.Parameters.AddWithValue("FileSize", file.FileSize);
                        cmd.Parameters.AddWithValue("FileUrl", file.FileUrl);
                        cmd.Parameters.AddWithValue("EmployeeId", file.EmployeeId);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                       await cmd.ExecuteNonQueryAsync();
                    }
                    sqlTran.Commit();
                    return true;
                }
                catch
                {
                    sqlTran.Rollback();
                    return false;
                }
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeFile>> GetAllAsync(PagedResultRequestDto input)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(EmployeeFile entity)
        {
            throw new NotImplementedException();
        }
    }
}
