using Microsoft.AspNetCore.Identity;

namespace TaskMangmentApi.Models
{
    public class ApplicationUser:IdentityUser
    {
        public List<Tasks> tasks { get; set; }
    }
}
