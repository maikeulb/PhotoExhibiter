using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Infra.Data.Context;
using System.Threading.Tasks;

namespace PhotoExhibiter.Infra.Data.Seed
{
    public class DbInitializer
    {
        public static async Task Initialize(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<DbInitializer> logger)
        {
            context.Database.EnsureCreated();

            if (context.Genres.Any())
            {
                return;
            }

            var genres = new Genre[]
            {
                Genre.Create("Land"),
                Genre.Create("Phil")
            };

            foreach (Genre g in genres)
            {
                context.Genres.Add(g);
            }
            context.SaveChanges();

            if (context.Users.Any())
            {
                return;
            }

            await CreateDefaultUserForApplication(userManager, logger);
        }

        private static async Task CreateDefaultUserForApplication(
            UserManager<ApplicationUser> userManager,
            ILogger<DbInitializer> logger)
        {
            const string email = "user@gmail.com";

            var user = await CreateDefaultUser(userManager, logger, email);
            await SetPasswordForDefaultUser(userManager,logger, email, user);
        }

        private static async Task<ApplicationUser> CreateDefaultUser (
             UserManager<ApplicationUser> userManager,
             ILogger<DbInitializer> logger,
             string email)
        {
            logger.LogInformation($"Create default user with email `{email}` for application");
            var user = ApplicationUser.Create(email, "user");

            var identityResult = await userManager.CreateAsync(user);
            if (identityResult.Succeeded)
            {
                logger.LogDebug($"Created default user `{email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Default user `{email}` cannot be created");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(identityResult));
                throw exception;
            }

            var createdUser = await userManager.FindByEmailAsync(email);
            return createdUser;
        }

        private static async Task SetPasswordForDefaultUser(
                UserManager<ApplicationUser> userManager,
                ILogger<DbInitializer> logger,
                string email,
                ApplicationUser user)
        {
            logger.LogInformation($"Set password for default user `{email}`");
            const string password = "P@ssw0rd!";
            var identityResult = await userManager.AddPasswordAsync(user, password);
            if (identityResult.Succeeded)
            {
                logger.LogTrace($"Set password `{password}` for default user `{email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Password for the user `{email}` cannot be set");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(identityResult));
                throw exception;
            }
        }

        private static string GetIdentiryErrorsInCommaSeperatedList(
            IdentityResult identityResult)
        {
            string errors = null;
            foreach (var identityError in identityResult.Errors)
            {
                errors += identityError.Description;
                errors += ", ";
            }
            return errors;
        }
    }
}
