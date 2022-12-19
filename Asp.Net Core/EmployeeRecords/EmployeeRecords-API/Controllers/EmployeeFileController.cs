using AutoMapper;
using EmployeeRecords_API.Errors;
using EmployeeRecords_Domain.Dtos.EmployeeFile;
using EmployeeRecords_Domain.Models;
using EmployeeRecords_Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EmployeeRecords_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeeFileController : Controller
    {
        private readonly IEmployeeFileRepository _employeeFileRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public EmployeeFileController(
            IEmployeeFileRepository employeeFileRepository,
            IWebHostEnvironment hostingEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper
            )
        {
            _employeeFileRepository = employeeFileRepository;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        [HttpPost] 
        public async Task<ActionResult<bool>> CreateAsync([FromForm]CreateEmployeeFileDto input)
        {
            var employeeFile = _mapper.Map<EmployeeFile>(input);
            if (input.File != null)
            {

                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "EmployeesFiles");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }
                string host = _httpContextAccessor.HttpContext.Request.Host.Value;

                string newfilename = Guid.NewGuid() + input.File.FileName;
                var filePath = Path.Combine(uploads, newfilename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                        await input.File.CopyToAsync(fileStream);
                    }
                    catch
                    {
                        return BadRequest(new ApiResponse(400, "file copy error"));
                    }

                }
                employeeFile.FileUrl = Path.Combine(host, "EmployeesFiles", newfilename);
                employeeFile.FileName = input.File.FileName;
                employeeFile.FileSize = input.File.Length;
            }
            return await _employeeFileRepository.AddAsync(employeeFile);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateListAsync([FromForm] CreateListEmployeeFileDto input)
        {
            List<EmployeeFile> employeeFiles = new List<EmployeeFile>();
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "EmployeesFiles");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }
                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                foreach(var file in input.Files)
                {
                    EmployeeFile empolyeeFile = new EmployeeFile();
                    string newfilename = Guid.NewGuid() + file.FileName;
                    var filePath = Path.Combine(uploads, newfilename);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                    try
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    catch
                    {
                        return BadRequest(new ApiResponse(400, "file copy error"));
                    }

                    }
                    empolyeeFile.FileUrl = Path.Combine(host, "EmployeesFiles", newfilename);
                    empolyeeFile.FileName = file.FileName;
                    empolyeeFile.FileSize = file.Length;
                    empolyeeFile.EmployeeId = input.EmployeeId;
                    employeeFiles.Add(empolyeeFile);
                }
                var result = await _employeeFileRepository.AddListAsync(employeeFiles);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new ApiResponse(400));
            }
        }
    }
}
