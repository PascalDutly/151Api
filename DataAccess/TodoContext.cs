using Microsoft.EntityFrameworkCore;
using Csbe.Todo.Api.Models;

namespace Csbe.Todo.Api.DataAccess
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<User> Users { get; set; }
    }
}