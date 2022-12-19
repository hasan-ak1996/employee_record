using EmployeeRecords_API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRecords_API.Controllers
{
    [Route("error/{code}")]
    // to ignore this controller fron swagger because not use this controller for client side
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        public IActionResult error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
