namespace TaskMangmentApi.Models.DTO.TaskDTO
{
    public class DisplayTaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DeadLine { get; set; }
        public string TaskStatus { get; set; }
        public string UserName { get; set; }
    
    }
}
