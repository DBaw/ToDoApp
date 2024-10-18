using Microsoft.EntityFrameworkCore;
using System;
using ToDoApp.Dto;

namespace ToDoApp.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserDto> Users { get; set; }
        public DbSet<NoteDto> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath;
            dbPath = System.IO.Path.Combine(AppContext.BaseDirectory, "ToDoApp.db");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}
