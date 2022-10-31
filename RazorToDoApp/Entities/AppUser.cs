using System.ComponentModel.DataAnnotations;

namespace RazorToDoApp.Entities
{
    public class AppUser
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(32)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public ICollection<AppTask> ToDoLists { get; set; }
    }
}
