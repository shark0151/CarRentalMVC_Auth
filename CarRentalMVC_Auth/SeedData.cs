using CarRentalMVC_Auth.Data;
using Microsoft.AspNetCore.Identity;

namespace CarRentalMVC_Auth
{
    public static class SeedData
    {
        public static void Seed(IServiceProvider serviceProvider,
            List<string> userList)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            
            SeedRole(roleManager);
            SeedUser(userManager);
            
        }
        public static void SeedUser(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                var user = new IdentityUser()
                {
                    UserName = "Admin",
                    Email = "admin@localhost"

                };
                var result = userManager.CreateAsync(user,"Password_1").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
            else
            {
                var user = userManager.FindByEmailAsync("admin@localhost").Result;
                userManager.AddToRoleAsync(user, "Administrator").Wait();
            }
        }
        public static void SeedRole(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"

                };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("RegisteredUser").Result)
            {
                var role = new IdentityRole
                {
                    Name = "RegisteredUser"

                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }

    }
}
