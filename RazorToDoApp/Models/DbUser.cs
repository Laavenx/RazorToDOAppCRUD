using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace RazorToDoApp.Models
{
    public class DbUser
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(32)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public ICollection<DbToDoLists> DbToDoLists { get; set; }
    }
}
