using AutoMapper;
using EmployeeRecords_API.Errors;
using EmployeeRecords_Domain.Dtos.Department;
using EmployeeRecords_Domain.Models;
using EmployeeRecords_Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeRecords_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(
            IDepartmentRepository departmentRepository,
            IMapper mapper
            )
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<DepartmentDto>>> GetAllAsync([FromQuery] PagedDepartmentResultRequestDto input)
        {
            var departments = await _departmentRepository.GetAllAsync(input);
            return Ok(_mapper.Map<List<DepartmentDto>>(departments)); 

        }
        [HttpPost]
        // to display responses types for this end point in swagger
        [ProducesResponseType(StatusCodes.Status200OK)]
        // if response type is 404 not found , response object is ApiResponse
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AddAsync(CreateDepartmentDto input)
        {
            var department = _mapper.Map<Department>(input);
            var result = await _departmentRepository.AddAsync(department);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new ApiResponse(400));
            }
        }

        [HttpPut]
        // to display responses types for this end point in swagger
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> UpdateAsync(UpdateDepartmentDto input)
        {
            var department = _mapper.Map<Department>(input);
            var result = await _departmentRepository.UpdateAsync(department);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new ApiResponse(400));
            }
        }
        [HttpDelete]
        // to display responses types for this end point in swagger
        [ProducesResponseType(StatusCodes.Status200OK)]
        // if response type is 404 not found , response object is ApiResponse
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            var result = await _departmentRepository.DeleteAsync(id);
            if (!result){
                return NotFound(new ApiResponse(404, "Not found entity with id: " + id));
            }
            else
            {
                return Ok(result);
            }
 
        }
    }
}
