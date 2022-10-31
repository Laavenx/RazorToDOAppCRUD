using System.ComponentModel.DataAnnotations;

namespace RazorToDoApp.Entities
{
    public class AppTask
    {
        public int Id { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
        public AppUser User { get; set; }
    }
}
