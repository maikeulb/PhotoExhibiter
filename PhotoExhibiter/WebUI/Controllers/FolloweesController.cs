using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoExhibiter.Application.Queries;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.WebUI.Controllers
{
    public class FolloweesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IApplicationUserRepository _repository;
        private readonly IMediator _mediator;

        public FolloweesController (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IApplicationUserRepository repository,
            IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<IActionResult> Followees (Followees.Query query)
        {
            query.UserId = _userManager.GetUserId (User);

            var model = await _mediator.Send (query);

            return View (model);
        }
    }
}