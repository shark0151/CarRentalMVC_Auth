using CarRentalMVC_Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentalMVC_Auth.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<Location>().ToTable("location");
            modelBuilder.Entity<Insurance>().ToTable("insurance");
            //modelBuilder.Entity<Student>().ToTable("Student");
        }

        public DbSet<Insurance> Insurance { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<CarRentalMVC_Auth.Models.Vehicle> Vehicle { get; set; }

    }
}