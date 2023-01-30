using DorisTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace DorisTodo
{
    public class TodoDbContext: DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        //public DbSet<Post> Posts { get; set; }

        public string DbPath { get; }

        public TodoDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);

            //"C:\\Users\\PC\\AppData\\Local\\todo.db";
            DbPath = System.IO.Path.Join(path, "todo.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
