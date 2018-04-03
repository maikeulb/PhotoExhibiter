using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PhotoExhibiter.Entities;
using System.Threading.Tasks;

namespace PhotoExhibiter.Data.Seed
{
    public class IdentityDbInitializer
    {
        public static async Task Initialize(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            // Default User
            var userEmail = configuration.GetSection("AppSettings")["UserEmail"];
            var userPassword = configuration.GetSection("AppSettings")["UserPassword"];
            var userName = "Gary";
            var userimageUrl = "http://www.atgetphotography.com/Images/Photos/GarryWinogrand/winogrand_8.jpg";

            var user = new ApplicationUser 
            { 
                UserName = userEmail,
                Email = userEmail,
                Name = userName,
                ImageUrl = userimageUrl
            };

            var existUser = await userManager.FindByEmailAsync(userEmail);

            if (existUser == null)
                await userManager.CreateAsync(user, userPassword);

            // Admin User
            var adminEmail = configuration.GetSection("AppSettings")["AdminEmail"];
            var adminPassword = configuration.GetSection("AppSettings")["AdminPassword"];
            var adminName = "Avedon";
            var adminImageUrl = "https://timenio.info/wp-content/uploads/2017/07/penn39.jpg";

            string adminRole = "Admin";
            IdentityResult adminRoleResult;

            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                Name = adminName,
                ImageUrl = adminImageUrl
            };

            var adminRoleExist = await roleManager.RoleExistsAsync(adminRole);
            if (!adminRoleExist)
                adminRoleResult = await roleManager.CreateAsync(new IdentityRole(adminRole));

            var existAdmin = await userManager.FindByEmailAsync(adminEmail);
            if (existAdmin == null)
            {
                var createAdmin = await userManager.CreateAsync(admin, adminPassword);
                if (createAdmin.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }

            // Demo Admin User
            var demoAdminEmail = configuration.GetSection("AppSettings")["DemoAdminEmail"];
            var demoAdminPassword = configuration.GetSection("AppSettings")["DemoAdminPassword"];
            var demoAdminName = "Penn";
            var demoAdminImageUrl = "https://i.pinimg.com/originals/af/ce/7c/afce7ca838d5c6c4b034e969d56d7037.jpg";

            string demoAdminRole = "DemoAdmin";
            IdentityResult demoAdminRoleResult;

            var demoAdmin = new ApplicationUser
            {
                UserName = demoAdminEmail,
                Email = demoAdminEmail,
                Name = demoAdminName,
                ImageUrl = demoAdminImageUrl
            };

            var demoAdminRoleExist = await roleManager.RoleExistsAsync(demoAdminRole);
            if (!demoAdminRoleExist)
                demoAdminRoleResult = await roleManager.CreateAsync(new IdentityRole(demoAdminRole));

            var existDemoAdmin = await userManager.FindByEmailAsync(demoAdminEmail);
            if (existDemoAdmin == null)
            {
                var createDemoAdmin = await userManager.CreateAsync(demoAdmin, demoAdminPassword);
                if (createDemoAdmin.Succeeded)
                    await userManager.AddToRoleAsync(demoAdmin, "DemoAdmin");
            }
        }
    }
}
