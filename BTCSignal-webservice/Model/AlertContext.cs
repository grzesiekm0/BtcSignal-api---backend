using System;
using Microsoft.EntityFrameworkCore;

namespace btcsignalwebservice.Model
{
    public class AlertContext : DbContext
    {
        public DbSet<Alert> Alert { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=blogging.db");
        }

    }
}

