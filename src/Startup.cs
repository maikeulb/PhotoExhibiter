using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhotoExhibiter.Infrastructure;
using PhotoExhibiter.Data.Context;
using PhotoExhibiter.Data.Repositories;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup (IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices (IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext> (options =>
                options.UseMySql (_config.GetConnectionString ("ApplicationConnectionString")));

            services.AddIdentity<ApplicationUser, IdentityRole> ()
                .AddEntityFrameworkStores<ApplicationDbContext> ()
                .AddDefaultTokenProviders ();

            services.AddTransient<IEmailSender, EmailSender> ();

            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository> ();
            services.AddScoped<IAttendanceRepository, AttendanceRepository> ();
            services.AddScoped<IExhibitRepository, ExhibitRepository> ();
            services.AddScoped<IFollowingRepository, FollowingRepository> ();
            services.AddScoped<IGenreRepository, GenreRepository> ();
            services.AddScoped<INotificationRepository, NotificationRepository> ();
            services.AddScoped<IUserNotificationRepository, UserNotificationRepository> ();

            services.AddMvc()
            /* services.AddMvc (options => */
                /* { */
                    /* options.Filters.Add (typeof (ValidatorActionFilter)); */
                /* }) */
                .AddFeatureFolders ()
                .AddFluentValidation (cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup> (); });

            services.AddMediatR ();
            services.AddAutoMapper ();
            Mapper.AssertConfigurationIsValid ();

            services.AddMemoryCache();
        }

        public void Configure (IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment ())
            {
                app.UseDeveloperExceptionPage ();
                app.UseDatabaseErrorPage ();
            }
            else
            {
                app.UseExceptionHandler ("/Home/Error");
            }

            app.UseStaticFiles ();

            app.UseAuthentication ();

            // app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvc (routes =>
            {
                routes.MapRoute (
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
