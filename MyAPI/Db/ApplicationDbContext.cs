using Microsoft.EntityFrameworkCore;
using MyAPI.Models;
using System;
using System.Net;
using System.Xml.Linq;

namespace MyAPI.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser()
            {
                Address = "MuzaffarName",
                Name = "Ravi",
                Password = "Ravikumar@1",
                Email = "ravik5@chetu.com",
                Id=1
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
