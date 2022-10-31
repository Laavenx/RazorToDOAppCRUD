using Microsoft.EntityFrameworkCore;
using RazorToDoApp.Entities;

namespace RazorToDoApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppTask> Tasks { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
