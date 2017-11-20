﻿using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhotoExhibiter.Application;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Domain.Models;
using PhotoExhibiter.Infrastructure;
using PhotoExhibiter.Infrastructure.Repositories;
using PhotoExhibiter.WebUI;

namespace PhotoExhibiter
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;

        public Startup (IConfiguration config, IHostingEnvironment env)
        {
            _config = config;
            _env = env;
        }

        public void ConfigureServices (IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole> ()
                .AddEntityFrameworkStores<ApplicationDbContext> ()
                .AddDefaultTokenProviders ();

            services.AddDbContext<ApplicationDbContext> (options =>
                options.UseMySql (_config.GetConnectionString ("ApplicationConnectionString")));

            services.AddTransient<IEmailSender, EmailSender> ();
            services.AddTransient<IExhibitService, ExhibitService> ();

            services.Configure<RazorViewEngineOptions> (options =>
                options.ViewLocationExpanders.Add (new WebUIViewLocationExpander ()));

            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository> ();
            services.AddScoped<IAttendanceRepository, AttendanceRepository> ();
            services.AddScoped<IExhibitRepository, ExhibitRepository> ();
            services.AddScoped<IFollowingRepository, FollowingRepository> ();
            services.AddScoped<IGenreRepository, GenreRepository> ();
            services.AddScoped<INotificationRepository, NotificationRepository> ();
            services.AddScoped<IUserNotificationRepository, UserNotificationRepository> ();

            services.AddMvc (options =>
                {
                    options.Filters.Add (typeof (ValidatorActionFilter));
                })
                .AddFluentValidation (cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup> (); });

            services.AddMediatR ();
            // services.AddTransient(typeof(IPipelineBehavior<Edit.Command,>), typeof(Edit.ValidationPipeline<Edit.Command,>));
            services.AddAutoMapper ();
            Mapper.AssertConfigurationIsValid ();
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