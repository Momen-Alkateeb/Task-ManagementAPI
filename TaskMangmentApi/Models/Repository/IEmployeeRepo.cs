using TaskMangmentApi.Models.DTO.TaskDTO;

namespace TaskMangmentApi.Models.Repository
{
    public interface IEmployeeRepo
    {
        public Task <List<DisplayTaskDTO>> GetOwnTask();
        public bool UpdateTaskStatus(string NewStauts,int id);
        public void Save();
    }
}
