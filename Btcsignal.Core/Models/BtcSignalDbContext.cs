
using Btcsignal.Core.Models.Dao;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Btcsignal.Core.Models
{
    public class BtcSignalDbContext : IdentityDbContext
    {
        public BtcSignalDbContext(DbContextOptions<BtcSignalDbContext> options)
                : base(options)
        {
        }

        public DbSet<Alert> Alerts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
