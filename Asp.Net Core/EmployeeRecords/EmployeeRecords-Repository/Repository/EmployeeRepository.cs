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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;

        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> AddAsync(Employee entity)
        {

            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_create_employee", connection);
                cmd.Parameters.AddWithValue("Name", entity.Name);
                cmd.Parameters.AddWithValue("DepartmentId", entity.DepartmentId);
                cmd.Parameters.AddWithValue("DateOfBirth", entity.DateOfBirth);
                cmd.Parameters.AddWithValue("Address", entity.Address);
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

        public async Task<int> AddWithGetIdAsync(Employee entity)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_create_employee", connection);
                cmd.Parameters.AddWithValue("Name", entity.Name);
                cmd.Parameters.AddWithValue("DepartmentId", entity.DepartmentId == null ? DBNull.Value : entity.DepartmentId);

                cmd.Parameters.AddWithValue("DateOfBirth", entity.DateOfBirth);
                cmd.Parameters.AddWithValue("Address", entity.Address);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
               // int status = await cmd.ExecuteNonQueryAsync();
                var NewId = await cmd.ExecuteScalarAsync();
                if (NewId != null)
                {

                    return (int)NewId;
                }
                else
                {
                    return -1;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_delete_employee", connection);
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

        public async Task<List<Employee>> GetAllAsync(PagedResultRequestDto input)
        {
            List<Employee> employees = new List<Employee>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_list_employee", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("skipCount", input.SkipCount * input.MaxResult);
                cmd.Parameters.AddWithValue("MaxResult",input.MaxResult);
                cmd.Parameters.AddWithValue("keyword", "%" + input.Keyword + "%" );
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        employees.Add(new Employee()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Name = dr["Name"].ToString(),
                            CreationTime = Convert.ToDateTime(dr["CreationTime"].ToString()),
                            Address = dr["Address"].ToString(),
                            DepartmentId = dr["DepartmentId"] != DBNull.Value ? Convert.ToInt32(dr["DepartmentId"]) : null,
                            DateOfBirth = dr["DateOfBirth"].ToString(),
                            
                            Department = new Department()
                            {
                                Name = dr["DepartmentName"].ToString()
                            }
                        });
                    }
                }
            }
            return employees;
        }

        public async Task<Employee> GetByIdWithNavigartionProperty(int id)
        {
            var dict = new Dictionary<int, Employee>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_employee_details", connection);
                cmd.Parameters.AddWithValue("Id", id);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        var EID = int.Parse(dr["Id"].ToString());
                        if (!dict.ContainsKey(EID))
                        {
                            var employee = new Employee()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Name = dr["Name"].ToString(),
                                CreationTime = Convert.ToDateTime(dr["CreationTime"].ToString()),
                                Address = dr["Address"].ToString(),
                                DepartmentId = dr["DepartmentId"] != DBNull.Value ? Convert.ToInt32(dr["DepartmentId"]): null,
                                DateOfBirth = dr["DateOfBirth"].ToString(),
                                Department = new Department()
                                {
                                    Name = dr["DepartmentName"].ToString()
                                }
                            };

                            dict[employee.Id] = employee;
                        }
                        var EmployeeFile = new EmployeeFile()
                        {
                            Id = dr["fileId"] != DBNull.Value ? Convert.ToInt32(dr["fileId"]) : default(int),
                            FileName = dr["FileName"]?.ToString(),
                            FileSize = dr["FileSize"] != DBNull.Value ?   Convert.ToDecimal(dr["FileSize"]) : default(int),
                            FileUrl = dr["FileUrl"]?.ToString(),
                            EmployeeId = dr["Id"] != DBNull.Value ? Convert.ToInt32(dr["Id"]) : default(int),
                            CreationTime = dr["fileCreationTime"] != DBNull.Value ?  Convert.ToDateTime(dr["fileCreationTime"].ToString()) :default(DateTime),
                        };
                        if(EmployeeFile.Id != 0)
                        {
                            dict[EID].EmployeeFiles.Add(EmployeeFile);
                        }

                    }
                }
            }
            var employeeResult = dict.Values.FirstOrDefault();
            return employeeResult;
        }

        public async Task<int> GetTotalCountAsync(PagedResultRequestDto input)
        {
            var totalCount = 0;
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_total_count_employees", connection);
                cmd.Parameters.AddWithValue("keyword", "%" + input.Keyword + "%");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    await dr.ReadAsync();
                    totalCount = Convert.ToInt32(dr["totalCount"]);
                }
            }
            return totalCount;
        }

        public async Task<bool> UpdateAsync(Employee entity)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();

                SqlTransaction sqlTran = connection.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_update_employee", connection);
                    cmd.Transaction = sqlTran;
                    cmd.Parameters.AddWithValue("Id", entity.Id);
                    cmd.Parameters.AddWithValue("Name", entity.Name);
                    cmd.Parameters.AddWithValue("DepartmentId", entity.DepartmentId == null ? DBNull.Value : entity.DepartmentId);
                    cmd.Parameters.AddWithValue("DateOfBirth", entity.DateOfBirth);
                    cmd.Parameters.AddWithValue("Address", entity.Address);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    await cmd.ExecuteNonQueryAsync();
                    if (entity.EmployeeFiles.Any())
                    {
                        foreach(var file in entity.EmployeeFiles)
                        {
                            cmd = new SqlCommand("sp_delete_employee_file", connection);
                            cmd.Transaction = sqlTran;
                            cmd.Parameters.AddWithValue("Id", file.Id);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            await cmd.ExecuteNonQueryAsync();
                        }
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
    }
}
