﻿using System;
using Btcsignal.Core.Models;
using Btcsignal.Core.Models.Dao;
using Microsoft.EntityFrameworkCore;

namespace btcsignalwebservice.Model
{
    [Obsolete("Not used any more", false)]
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
