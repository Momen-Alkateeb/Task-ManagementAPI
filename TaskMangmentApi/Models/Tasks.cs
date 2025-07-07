namespace TaskMangmentApi.Models
{
    public class Tasks
    {
       
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DeadLine { get; set; }
        public  string TaskStatus { get; set; }
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        
    }
}
