using System.ComponentModel.DataAnnotations;

namespace RazorToDoApp.Models
{
    public class LoginCredentials
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
