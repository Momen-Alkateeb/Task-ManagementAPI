using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskMangmentApi.Models.Dbinfo;
using TaskMangmentApi.Models.DTO.TaskDTO;

namespace TaskMangmentApi.Models.Reposotory
{
    public class TaskRepository:ITaskRepo
    {
        AppDbContext _context;
        UserManager<ApplicationUser> _usermanager;
        public TaskRepository(AppDbContext Context , UserManager<ApplicationUser>User)
        {
            _usermanager = User;
            _context = Context;
            
        }
        public async Task<List<DisplayTaskDTO>> GetAllTasks()
        {
            var tasks = await _context.Task.ToListAsync(); 

            List<DisplayTaskDTO> taskDTO = new List<DisplayTaskDTO>();

            foreach (var item in tasks)
            {
                DisplayTaskDTO task = new DisplayTaskDTO
                {
                    Id = item.Id,
                    Title = item.Title, 
                    Description = item.Description,
                    DeadLine = item.DeadLine,
                    TaskStatus = item.TaskStatus
                };

                var userForTask = await _usermanager.FindByIdAsync(item.UserId);
                task.UserName = userForTask?.UserName ?? "Unknown"; // 👈 handle null case

                taskDTO.Add(task);
            }

            return taskDTO;
        }

        public async Task<List<DisplayTaskDTO>> GetTaskByEmployee(string EmpName)
        {
            var user = await _usermanager.FindByNameAsync(EmpName);
            if (user == null)
            {
                return null;
            }
            var task = _context.Task.Where(x => x.UserId == user.Id.ToString());
            List<DisplayTaskDTO> taskDTO = new List<DisplayTaskDTO>();
            foreach(var item in task)
            {
                DisplayTaskDTO Task = new DisplayTaskDTO();
                Task.Id = item.Id;
                Task.Title = item.Description;
                Task.Description = item.Description;
                Task.DeadLine = item.DeadLine;
                Task.TaskStatus = item.TaskStatus;
                Task.UserName = EmpName;
                taskDTO.Add(Task);

            }
            return taskDTO;

        }
        public async Task< DisplayTaskDTO> GetTaskByTaskId(int id)
        {
            var Task = _context.Task.FirstOrDefault(x => x.Id == id);
            if (Task == null)
            {
                return null;
            }
            var user = await _usermanager.FindByIdAsync(Task.UserId);
            DisplayTaskDTO taskDTO = new DisplayTaskDTO();
            taskDTO.Id = Task.Id;
            taskDTO.Title = Task.Title;
            taskDTO.Description = Task.Description;
            taskDTO.DeadLine = Task.DeadLine;
            taskDTO.TaskStatus = Task.TaskStatus;
            taskDTO.UserName = user.UserName!;

            return taskDTO;

        }

        public  async Task<int> AddTask(AddingTaskDTO newtask)
        {
            var NewTask = new Tasks();
            NewTask.Title = newtask.Title;
            NewTask.Description = newtask.Description;
            NewTask.DeadLine = newtask.DeadLine;
            NewTask.TaskStatus = newtask.TaskStatus;
            var user = await _usermanager.FindByNameAsync(newtask.UserName);
            NewTask.UserId = user.Id;
            _context.Task.Add(NewTask);
            Save();
            return NewTask.Id;
            
        }
        public async Task<int> UpdateTaskByTaskId(int TaskId ,AddingTaskDTO newtask)
        {
            Tasks oldTask = _context.Task.FirstOrDefault(T => T.Id == TaskId)!;
            if (oldTask == null)
            {
                return 0;
            }
            oldTask.Title = newtask.Title;
            oldTask.Description = newtask.Description;
            oldTask.DeadLine = newtask.DeadLine;
            oldTask.TaskStatus = newtask.TaskStatus;
            var user = await _usermanager.FindByNameAsync(newtask.UserName);
            oldTask.UserId = user.Id;
            _context.Task.Update(oldTask);
            Save();
            return oldTask.Id;
        }
        public async Task<int> UpdateTaskByEmployee(string EmpName, AddingTaskDTO newtask)
        {
            var User = await _usermanager.FindByNameAsync(EmpName);
            if(User == null)
            {
                return 0;
            }
            var oldTask = _context.Task.FirstOrDefault(T => T.UserId == User.Id);
            if (oldTask == null)
            {
                return 0;
            }
            oldTask.Title = newtask.Title;
            oldTask.Description = newtask.Description;
            oldTask.DeadLine = newtask.DeadLine;
            oldTask.TaskStatus = newtask.TaskStatus;
            var user = await _usermanager.FindByNameAsync(newtask.UserName);
            oldTask.UserId = user.Id;
            _context.Task.Update(oldTask);
            Save();
            return oldTask.Id;
        }
        public bool RemoveTaskById(int id)
        {
            var Task = _context.Task.FirstOrDefault(x => x.Id == id);
            if (Task == null)
            {
                return false;
            }
            _context.Task.Remove(Task);
            Save();
            return true;
        }
        public async Task<bool> RemoveTaskByEmployee(string EmpName)
        {
            var user =await _usermanager.FindByNameAsync(EmpName);
            if (user == null)
            {
                return false;
            }
            var Task = _context.Task.FirstOrDefault(T => T.UserId == user.Id);
            if (Task == null)
            {
                return false;
            }
            _context.Task.Remove(Task);
            Save();
            return true;

        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
