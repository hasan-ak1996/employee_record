using AutoMapper;
using EmployeeRecords_API.Errors;
using EmployeeRecords_Domain.Dtos;
using EmployeeRecords_Domain.Dtos.Employee;
using EmployeeRecords_Domain.Models;
using EmployeeRecords_Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeRecords_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _empolyeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(
            IEmployeeRepository empolyeeRepository,
            IMapper mapper
            )
        {
            _empolyeeRepository = empolyeeRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<PagedResultDto<EmployeeDto>> GetAllAsync([FromQuery]PagedEmployeeResultRequestDto input)
        {
            var totalCount = await _empolyeeRepository.GetTotalCountAsync(input);
            var employees = await _empolyeeRepository.GetAllAsync(input);

            return new PagedResultDto<EmployeeDto>()
            {
                TotalCount = totalCount,
                Items = _mapper.Map<List<EmployeeDto>>(employees)
            };
        }
          
        [HttpPost]
        // to display responses types for this end point in swagger
        [ProducesResponseType(StatusCodes.Status200OK)]
        // if response type is 404 not found , response object is ApiResponse
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AddAsync(CreateEmployeeDto input)
        {
                var employee = _mapper.Map<Employee>(input);
                var result = await _empolyeeRepository.AddAsync(employee);
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ApiResponse(400));
                }
          

        }
        [HttpPost]
        // to display responses types for this end point in swagger
        [ProducesResponseType(StatusCodes.Status200OK)]
        // if response type is 404 not found , response object is ApiResponse
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> AddWithGetIdAsync(CreateEmployeeDto input)
        {
            var employee = _mapper.Map<Employee>(input);
            var result =await _empolyeeRepository.AddWithGetIdAsync(employee);
            if (result != -1)
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
        // if response type is 404 not found , response object is ApiResponse
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> UpdateAsync(UpdateEmployeeDto input)
        {
            var employee = _mapper.Map<Employee>(input);
            var result = await _empolyeeRepository.UpdateAsync(employee);
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
            var result = await _empolyeeRepository.DeleteAsync(id);
            if(result)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(new ApiResponse(404,"Not found entity with id: " + id));
            }
        }
        [HttpGet]
        // to display responses types for this end point in swagger
        [ProducesResponseType(StatusCodes.Status200OK)]
        // if response type is 404 not found , response object is ApiResponse
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDto>> GetByIdWithNavigartionProperty(int id)
        {
            var employee = await _empolyeeRepository.GetByIdWithNavigartionProperty(id);
            if(employee == null)
            {
                return NotFound(new ApiResponse(404, "Not found entity with id: " + id));
            }
            else
            {
                return _mapper.Map<EmployeeDto>(employee);
            }

        }
    }
}
