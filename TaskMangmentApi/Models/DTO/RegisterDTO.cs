using System.ComponentModel.DataAnnotations;

namespace TaskMangmentApi.Models.DTO
{
    public class RegisterDTO
    {
        
        [Required]
        [Display(Name =" Full Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public string Status { get; set; }
    }
}
