using API.Model;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profiling> Profiling { get; set; }

        public DbSet<Education> Education { get; set; }

        public DbSet<University> University { get; set; }
        public DbSet<Role> Role { get; set; }

        public DbSet<AccountRole> AccountRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>()
                .HasOne(account => account.Account)
                .WithOne(employee => employee.Employee)
                .HasForeignKey<Account>(account => account.NIK);

            modelBuilder.Entity<Account>()
                .HasOne(profiling => profiling.Profiling)
                .WithOne(account => account.Account)
                .HasForeignKey<Profiling>(account => account.NIK);

            modelBuilder.Entity<Profiling>()
                .HasOne(education => education.Education)
                .WithMany(profiling => profiling.Profilings);


            modelBuilder.Entity<University>()
                .HasMany(education => education.Educations)
                .WithOne(university => university.University);

            modelBuilder.Entity<Account>()
                .HasMany(c => c.accountrole)
                .WithOne(e => e.Account);


            //one to many role to accountrole
            modelBuilder.Entity<Role>()
                .HasMany(c => c.AccountRoles)
                .WithOne(e => e.Role);


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}