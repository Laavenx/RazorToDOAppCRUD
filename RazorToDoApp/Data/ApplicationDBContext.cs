using Microsoft.EntityFrameworkCore;
using RazorToDoApp.Models;

namespace RazorToDoApp.Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<DbUser> User { get; set; }
        public DbSet<DbToDoTask> ToDoTask { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
    }
}
