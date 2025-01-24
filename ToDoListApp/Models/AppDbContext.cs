using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

public class AppDbContext : DbContext
{

    public DbSet<ToDoTask> ToDoTasks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
