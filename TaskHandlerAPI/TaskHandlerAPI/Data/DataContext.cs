using Microsoft.EntityFrameworkCore;
using TaskHandlerAPI.Models;

namespace TaskHandlerAPI.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<TaskToDo> TasksToDo { get; set; }
  }
}
