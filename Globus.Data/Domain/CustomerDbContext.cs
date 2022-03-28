using Globus.Data.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globus.Data.Domain
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<LGA> LGAs { get; set; }


        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
           

           

            modelBuilder.Entity<LGA>()
            .HasOne(p => p.State)
            .WithMany(b => b.lgas)
            .HasForeignKey(p => p.StateId);

        }
    }
}
