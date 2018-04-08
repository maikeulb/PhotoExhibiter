using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PhotoExhibiter.Features.ManageExhibits
{
    public class ManageExhibitsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public ManageExhibitsController (IMediator mediator,
            ILogger<ManageExhibitsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize(Roles="Admin, DemoAdmin")]
        public IActionResult Index ()
        {
            return View ();
        }

        [Authorize(Roles="Admin, DemoAdmin")]
        public async Task<IActionResult> Details (Details.Query query)
        {
            var modelOrError = await _mediator.Send (query);

            return modelOrError.IsSuccess
                ? (IActionResult)View(modelOrError.Value)
                : (IActionResult)BadRequest(modelOrError.Error);
        }

        [Authorize(Roles="Admin, DemoAdmin")]
        public async Task<IActionResult> Edit (Edit.Query query)
        {
            var modelOrError = await _mediator.Send (query);

            return modelOrError.IsSuccess
                ? (IActionResult)View("Edit", modelOrError.Value)
                : (IActionResult)BadRequest(modelOrError.Error);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Edit (Edit.Command command)
        {
            var result = await _mediator.Send (command);

            return result.IsSuccess
                ? (IActionResult)RedirectToAction ("Index")
                : (IActionResult)BadRequest(result.Error);
        }
    }
}
