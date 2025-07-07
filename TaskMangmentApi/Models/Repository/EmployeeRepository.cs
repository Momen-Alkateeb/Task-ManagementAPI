using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskMangmentApi.Models.Dbinfo;
using TaskMangmentApi.Models.DTO.TaskDTO;

namespace TaskMangmentApi.Models.Repository
{
    public class EmployeeRepository:IEmployeeRepo
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _usermanger;
        private readonly IHttpContextAccessor _httpaccessor;
        public EmployeeRepository(AppDbContext Context , UserManager<ApplicationUser>UserManager, 
            IHttpContextAccessor HttpAccessor)
        {
            _context = Context;
            _usermanger = UserManager;
            _httpaccessor = HttpAccessor;
            
        }
        public async Task<List<DisplayTaskDTO>> GetOwnTask()
        {
            var userId = _httpaccessor.HttpContext?.User?
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return new List<DisplayTaskDTO>();

            var user = await _usermanger.FindByIdAsync(userId);
            if (user == null)
                return new List<DisplayTaskDTO>();

            var tasks = await _context.Task
                .Where(t => t.UserId == userId)
                .ToListAsync(); 

            var result = tasks.Select(task => new DisplayTaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DeadLine = task.DeadLine,
                UserName = user.UserName,
                TaskStatus = task.TaskStatus
            }).ToList();

            return result;
        }
        public bool UpdateTaskStatus(string NewStauts, int id)
        {  
          Tasks task = _context.Task.FirstOrDefault(T => T.Id == id)!;
            if (task == null)
                return false;
            task.TaskStatus = NewStauts;
            _context.Task.Update(task);
            Save();
            return true;
           
            

        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
