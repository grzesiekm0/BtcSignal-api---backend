using System;
using Microsoft.EntityFrameworkCore;

namespace btcsignalwebservice.Model
{
    public class BtcSignalDbContext : DbContext
{
        public BtcSignalDbContext(DbContextOptions<BtcSignalDbContext> options)
                : base(options)
        {
        }

        public DbSet<Alert> Alerts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
