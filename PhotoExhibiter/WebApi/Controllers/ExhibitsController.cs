using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Domain.Models;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.WebApi.Commands;

namespace PhotoExhibiter.WebApi.Apis
{
    [Route ("api/[Controller]")]
    [Authorize]
    public class ExhibitsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AttendancesController> _logger;
        private readonly IExhibitRepository _repository;
        private readonly IMediator _mediator;

        public ExhibitsController (
            UserManager<ApplicationUser> userManager,
            ILogger<AttendancesController> logger,
            IExhibitRepository repository,
            IMediator mediator)
        {
            _userManager = userManager;
            _logger = logger;
            _repository = repository;
            _mediator = mediator;
        }

        [HttpDelete]
        public async Task <IActionResult> Cancel (Cancel.Command command)
        {
            try
            {
                // validation
                var userId = _userManager.GetUserId (User);
                var exhibit = _repository.GetExhibitWithAttendees (command.ExhibitId);
                if (exhibit == null || exhibit.IsCanceled)
                    return NotFound ();

                if (exhibit.PhotographerId != userId)
                    return Unauthorized ();
                // validation

                await _mediator.Send(command);

                return Ok ();
            }
            catch (Exception ex)
            {
                _logger.LogError ($"Failed to add attendee: {ex}");
            }

            return BadRequest ("Failed to Cancel Exhibit");
        }
    }
}
