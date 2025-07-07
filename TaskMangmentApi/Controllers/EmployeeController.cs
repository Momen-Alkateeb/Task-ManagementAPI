using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using TaskMangmentApi.Models.Repository;

namespace TaskMangmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo _EmpRepo;
        public EmployeeController(IEmployeeRepo EmpRepo)
        {
            _EmpRepo = EmpRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var Tasks =await _EmpRepo.GetOwnTask();
            if (Tasks == null)
            {
                return BadRequest("The User is not Valied");
            }
            return Ok(Tasks);

        }
        [HttpPatch("{id:int}")]
        public IActionResult UpdateTaskStatus([FromBody]string TaskName , int id)
        {
            var Result = _EmpRepo.UpdateTaskStatus(TaskName, id);
            if (Result)
                return NoContent();
           return BadRequest("The Task not Found!");
        }
    }
}
