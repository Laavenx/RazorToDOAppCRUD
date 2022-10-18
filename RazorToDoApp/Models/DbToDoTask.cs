using System.ComponentModel.DataAnnotations;

namespace RazorToDoApp.Models
{
    public class DbToDoTask
    {
        public int Id { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
        public DbUser User { get; set; }
    }
}
