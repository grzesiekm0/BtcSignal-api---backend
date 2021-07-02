using System;
using Btcsignal.Core.Models.Dao;
using Microsoft.EntityFrameworkCore;

namespace btcsignalwebservice.Model
{
    [Obsolete("Not used any more", false)]
    public class AlertContext : DbContext
    {
        public AlertContext(DbContextOptions<AlertContext> options)
            : base(options)
        {
        }

        public DbSet<Alert> Alerts { get; set; }
    }
}
