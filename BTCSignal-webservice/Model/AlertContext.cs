using System;
using Microsoft.EntityFrameworkCore;

namespace btcsignalwebservice.Model
{
    public class AlertContext : DbContext
    {
        public AlertContext(DbContextOptions<AlertContext> options)
            : base(options)
        {
        }

        public DbSet<Alert> Alerts { get; set; }
    }
}
