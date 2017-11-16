using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.WebApi.ApiModels;

namespace PhotoExhibiter.WebApi.Apis
{
    [Route ("api/[Controller]")]
    [Authorize]
    public class ExhibitsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AttendancesController> _logger;
        private readonly IExhibitRepository _repository;

        public ExhibitsController (
            UserManager<ApplicationUser> userManager,
            ILogger<AttendancesController> logger,
            IExhibitRepository repository)
        {
            _userManager = userManager;
            _logger = logger;
            _repository = repository;
        }

        [HttpDelete]
        public IActionResult Cancel (int id)
        {
            try
            {
                var userId = _userManager.GetUserId (User);
                var exhibit = _repository.GetExhibitWithAttendees (id);

                if (exhibit == null || exhibit.IsCanceled)
                    return NotFound ();

                if (exhibit.PhotographerId != userId)
                    return Unauthorized ();

                _logger.LogInformation ("Getting UserId {ID}", userId);

                exhibit.Cancel ();

                _repository.SaveAll ();

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
