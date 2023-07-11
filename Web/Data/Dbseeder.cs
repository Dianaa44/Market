using DatabaseContext.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Web.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed roles
            var roles = new[] { "Admin" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
             
            // Seed users
            //var email = "admin@admin.com";
            //var password = "test1234$$SS";
            //if (await userManager.FindByEmailAsync(email) == null)
            //{
            //    var user = new ApplicationUser { UserName = email, Email = email };
            //    var result = await userManager.CreateAsync(user, password);
            //    if (result.Succeeded)
            //    {
            //        await userManager.AddToRoleAsync(user, "Admin");
            //    }
            //}
        }
    }
}