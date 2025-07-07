using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using TaskMangmentApi.Models.DTO.TaskDTO;
using TaskMangmentApi.Models.Reposotory;

namespace TaskMangmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepo _TaskRepo;
        public TasksController(ITaskRepo Repo)
        {
            _TaskRepo = Repo;
        }
        [HttpGet]
        public async Task< IActionResult> GetAllTask()
        {
            var Tasks = await _TaskRepo.GetAllTasks();
            return Ok(Tasks);
        }
        [HttpGet("ByEmployeeName/{EmployeeName:alpha}")]
        public async Task<IActionResult> GetTaskbByEmployeeName(string EmployeeName)
        {
            var Task = await  _TaskRepo.GetTaskByEmployee(EmployeeName);
            if (Task == null)
            {
                return BadRequest("The Task Not Found, Make Sure the Employee Name");
            }
            return Ok(Task);

        }
        [HttpGet("ByTaskId/{Taskid:int}")]
        public async Task< IActionResult> GetTaskById(int Taskid)
        {
            var task = await _TaskRepo.GetTaskByTaskId(Taskid);
            if (task == null)
            {
                return BadRequest("The Task Not Found");
            }
            return Ok(task);
        }
        [HttpPost]
        public async Task<IActionResult> AddingNewTask(AddingTaskDTO newTask)
        {
            if (ModelState.IsValid)
            {

                int TaskID = await _TaskRepo.AddTask(newTask);
                return Created();

            }
            return BadRequest(ModelState);
        }
        [HttpPut("ById/{TaskID:int}")]
        public async Task< IActionResult> UpdateTaskById(int Taskid , AddingTaskDTO newTask)
        {
            if (ModelState.IsValid)
            {

            int TaskId = await _TaskRepo.UpdateTaskByTaskId(Taskid, newTask);
            if (TaskId != 0)
            {
                return Ok("Updated Successfully");
            }
            }
            return BadRequest(ModelState);
        }
        [HttpPut("ByName/{Empname:alpha}")]
        public async Task<IActionResult> UpdateTaskByEmpName(string EmpName, AddingTaskDTO newTask)
        {
            if (ModelState.IsValid)
            {

                int TaskId = await _TaskRepo.UpdateTaskByEmployee(EmpName, newTask);
                if (TaskId != 0)
                {
                    return Ok("Updated Successfully");
                }
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("ById/{id:int}")]
        public IActionResult RemoveTaskById(int id)
        {
            if (_TaskRepo.RemoveTaskById(id))
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete("ByName/{EmpName:alpha}")]
        public async Task< IActionResult> RemoveByName(string EmpName)
        {
            if(await _TaskRepo.RemoveTaskByEmployee(EmpName))
            {
                return Ok();
            }
            return BadRequest();
        } 
    }
}
