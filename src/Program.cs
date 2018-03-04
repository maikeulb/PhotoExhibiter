﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Data.Seed;
using PhotoExhibiter.Data.Context;

namespace PhotoExhibiter
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host = BuildWebHost(args);

           using (var scope = host.Services.CreateScope())
           {
               var services = scope.ServiceProvider;
               try
               {
                   var applicationDbContext = services.GetRequiredService<ApplicationDbContext>();
                   var applicationDbInitializerLogger = services.GetRequiredService<ILogger<ApplicationDbInitializer>>();
                   ApplicationDbInitializer.Initialize(applicationDbContext, applicationDbInitializerLogger).Wait();

                   var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                   var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                   var configuration = services.GetRequiredService<IConfiguration>();
                   var identityDbInitializerLogger = services.GetRequiredService<ILogger<IdentityDbInitializer>>();
                   IdentityDbInitializer.Initialize(userManager, roleManager, configuration).Wait();
               }
               catch (Exception ex)
               {
                   var logger = services.GetRequiredService<ILogger<Program>>();
                   logger.LogError(ex, "An error occurred while seeding the database.");
               }
           }
           host.Run();
        }

        public static IWebHost BuildWebHost (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .UseStartup<Startup> ()
            .Build ();
    }
}
