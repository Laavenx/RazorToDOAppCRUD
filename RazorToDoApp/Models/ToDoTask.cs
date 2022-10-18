using System.ComponentModel.DataAnnotations;

namespace RazorToDoApp.Models
{
    public class ToDoTask
    {
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
