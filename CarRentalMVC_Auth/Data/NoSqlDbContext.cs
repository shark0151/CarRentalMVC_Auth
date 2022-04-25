using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentalMVC_Auth.Data
{
    public class NoSqlDbContext : IdentityDbContext
    {
        public NoSqlDbContext(DbContextOptions<NoSqlDbContext> options)
            : base(options)
        {
        }

    }
    
}
