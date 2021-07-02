
using Btcsignal.Core.Models.Dao;
using Microsoft.EntityFrameworkCore;

namespace Btcsignal.Core.Models
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
