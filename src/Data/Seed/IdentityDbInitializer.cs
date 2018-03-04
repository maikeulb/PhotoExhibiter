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
            var userPassword = configuration.GetSection("AppSettings")["UserPassword"];
            var userEmail = configuration.GetSection("AppSettings")["UserEmail"];
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
            var adminPassword = configuration.GetSection("AppSettings")["AdminPassword"];
            var adminEmail = configuration.GetSection("AppSettings")["AdminEmail"];
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
            var demoAdminPassword = configuration.GetSection("AppSettings")["DemoAdminPassword"];
            var demoAdminEmail = configuration.GetSection("AppSettings")["DemoAdminEmail"];
            var demoAdminName = "Penn";
            var demoAdminImageUrl = "https://static1.squarespace.com/static/54f5f960e4b0c6b83de5dc30/t/57aa5a89e6f2e16320a9a2fa/1470782091560/";
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
            if (demoAdmin == null)
            {
                var createDemoAdmin = await userManager.CreateAsync(demoAdmin, demoAdminPassword);
                if (createDemoAdmin.Succeeded)
                    await userManager.AddToRoleAsync(demoAdmin, "DemoAdmin");
            }
        }
    }
}
