using Btcsignal.Core.Models;
using Btcsignal.Core.Models.Dao;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace btcsignalwebservice.Model
{
    public class AplicationDbContext : IdentityDbContext
{

    public AplicationDbContext(DbContextOptions options)
            : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    
}
}
