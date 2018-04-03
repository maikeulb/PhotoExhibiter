using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Data.Context;
using System.Threading.Tasks;

namespace PhotoExhibiter.Data.Seed
{
    public class ApplicationDbInitializer
    {
        public static async Task Initialize(
            ApplicationDbContext context,
            ILogger<ApplicationDbInitializer> logger)
        {
            context.Database.EnsureCreated();

            if (!context.Genres.Any())
            {
                context.Genres.AddRange(
                    GetPreconfiguredGenres());

                await context.SaveChangesAsync();
            }
        }

        static IEnumerable<Genre> GetPreconfiguredGenres()
        {
            return new List<Genre>()
            {
                Genre.Create("Landscape"),
                Genre.Create("Street"),
                Genre.Create("Portrait"),
                Genre.Create("Photojournalism")
            };
        }
    }
}
