using CSharpFunctionalExtensions;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Data.Context;
using PhotoExhibiter.Features;
using PhotoExhibiter.Features.Account;
using PhotoExhibiter.Features.Home;
using PhotoExhibiter.Entities;

namespace PhotoExhibiter.Features.ManageExhibits
{
    public class ManageExhibitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManageExhibitsController (ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index ()
        {
            return View ();
        }

    }
}
