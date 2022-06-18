using CarRentalMVC_Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarRentalMVC_Auth.Models.Document;

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
            modelBuilder.Entity<Rental>().ToTable("rental");
            modelBuilder.Entity<Vehicle>().ToTable("vehicle");
            //modelBuilder.Entity<Student>().ToTable("Student");
        }

        public DbSet<Insurance> Insurance { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        //public DbSet<CarRentalMVC_Auth.Models.Document.Rental_Doc>? Rental_Doc { get; set; }
        //public DbSet<CarRentalMVC_Auth.Models.Document.Location_Doc>? Location_Doc { get; set; }
        //public DbSet<CarRentalMVC_Auth.Models.Document.Insurance_Doc>? Insurance_Doc { get; set; }
        //public DbSet<CarRentalMVC_Auth.Models.Document.User_Doc>? User_Doc { get; set; }

    }
}