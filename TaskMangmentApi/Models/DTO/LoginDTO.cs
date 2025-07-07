using System.ComponentModel.DataAnnotations;

namespace TaskMangmentApi.Models.DTO
{
    public class LoginDTO
    {
        [Required]
        [Display(Name ="UserName / Email")]
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }

    }
}
