using System.ComponentModel.DataAnnotations;

namespace TaskMangmentApi.Models.DTO.TaskDTO
{
    public class AddingTaskDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DeadLine { get; set; }
        [Required]
        public string TaskStatus { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
