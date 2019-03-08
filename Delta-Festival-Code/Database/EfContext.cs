using Database.Mapping;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Database
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions<EfContext> options)
          : base(options)
        {

        }
        public virtual  DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
                 //modelBuilder.Entity<User>().HasKey(p=>p.Id);
            //modelBuilder.ApplyConfiguration(new UserMap());
        }

    }
}
