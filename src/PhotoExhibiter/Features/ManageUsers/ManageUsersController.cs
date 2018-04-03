using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhotoExhibiter.Features.ManageUsers
{
    public class ManageUsersController : Controller
    {
        private readonly IMediator _mediator;

        public ManageUsersController (IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize (Roles = "Admin, DemoAdmin")]
        public IActionResult Index ()
        {
            return View ();
        }

        [Authorize (Roles = "Admin, DemoAdmin")]
        public async Task<IActionResult> Details (Details.Query query)
        {
            var modelOrError = await _mediator.Send (query);

            return modelOrError.IsSuccess ?
                (IActionResult) View (modelOrError.Value) :
                (IActionResult) BadRequest (modelOrError.Error);
        }

        [Authorize (Roles = "Admin, DemoAdmin")]
        public async Task<IActionResult> Edit (Edit.Query query)
        {
            var modelOrError = await _mediator.Send (query);

            return modelOrError.IsSuccess ?
                (IActionResult) View (modelOrError.Value) :
                (IActionResult) BadRequest (modelOrError.Error);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> Edit (Edit.Command command)
        {
            var result = await _mediator.Send (command);

            return result.IsSuccess ?
                (IActionResult) RedirectToAction ("Index") :
                (IActionResult) BadRequest (result.Error);
        }
    }
}