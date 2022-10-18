using System.ComponentModel.DataAnnotations;

namespace RazorToDoApp.Models
{
    public class ToDoTask
    {
        [MaxLength(30)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
