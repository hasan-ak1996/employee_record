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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConfiguration _configuration;

        public DepartmentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> AddAsync(Department entity)
        {
            using(var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_create_department", connection);
                cmd.Parameters.AddWithValue("Name", entity.Name);
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

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_delete_department", connection);
                cmd.Parameters.AddWithValue("Id", id);
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

        public async Task<List<Department>> GetAllAsync(PagedResultRequestDto input)
        {
            List<Department> departments = new List<Department>();
            using(var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_list_departments", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("skipCount", input.SkipCount * input.MaxResult);
                cmd.Parameters.AddWithValue("MaxResult", input.MaxResult);
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while(await dr.ReadAsync())
                    {
                        departments.Add(new Department()
                        {
                            Id =Convert.ToInt32(dr["Id"]),
                            Name = dr["Name"].ToString(),
                            CreationTime = Convert.ToDateTime(dr["CreationTime"].ToString()),
                        });
                    }
                }
            }
            return departments;
        }

        public async Task<bool> UpdateAsync(Department entity)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_update_department", connection);
                cmd.Parameters.AddWithValue("Id", entity.Id);
                cmd.Parameters.AddWithValue("Name", entity.Name);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                int status = await cmd.ExecuteNonQueryAsync();
                var e = cmd.ExecuteNonQueryAsync().Exception.Message;
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
    }
}
