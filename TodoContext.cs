using Microsoft.EntityFrameworkCore;
using TodoListAPI.Models;

namespace TodoListAPI
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
    }
}
