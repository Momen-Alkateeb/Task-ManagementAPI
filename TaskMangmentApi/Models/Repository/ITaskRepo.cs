using TaskMangmentApi.Models.DTO.TaskDTO;

namespace TaskMangmentApi.Models.Reposotory
{
    public interface ITaskRepo
    {
        public Task<List<DisplayTaskDTO>> GetAllTasks();
        public Task<List<DisplayTaskDTO>> GetTaskByEmployee(string EmpName);
        public Task< DisplayTaskDTO> GetTaskByTaskId(int id);

        public Task<int> AddTask(AddingTaskDTO newtask);
        public Task<int> UpdateTaskByTaskId(int TaskId, AddingTaskDTO newtask);
        public Task<int> UpdateTaskByEmployee(string EmpName, AddingTaskDTO newtask);

        public bool RemoveTaskById(int id);
        public Task <bool> RemoveTaskByEmployee(string EmpName);
        public void Save();
    }
}
