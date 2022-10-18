using System.ComponentModel.DataAnnotations;

namespace RazorToDoApp.Models
{
    public class RegisterCredentials
    {
        [Required]
        [MinLength(5)]
        [MaxLength(32)]
        public string UserName { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(32)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
