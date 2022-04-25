#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarRentalMVC_Auth.Models;

namespace CarRentalMVC_Auth.Data
{
    public class CarRentalMVC_AuthContext : DbContext
    {
        public CarRentalMVC_AuthContext (DbContextOptions<CarRentalMVC_AuthContext> options)
            : base(options)
        {
        }

        public DbSet<CarRentalMVC_Auth.Models.Car> Car { get; set; }
    }
}
