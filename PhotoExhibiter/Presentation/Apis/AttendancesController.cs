using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Presentation.ApiModels;

namespace PhotoExhibiter.Presentation.Apis
{
    [Route ("api/[Controller]")]
    [Authorize]
    public class AttendancesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AttendancesController> _logger;
        private readonly IAttendanceRepository _repository;

        public AttendancesController (
            UserManager<ApplicationUser> userManager,
            ILogger<AttendancesController> logger,
            IAttendanceRepository repository)
        {
            _logger = logger;
            _userManager = userManager;
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Attend ([FromBody] AttendanceApiModel model)
        {
            try
            {
                var userId = _userManager.GetUserId (User);

                var attendance = _repository.GetAttendance (model.ExhibitId, userId);
                if (attendance != null)
                    return BadRequest ("The attendance already exists.");

                _logger.LogInformation ("Getting UserId {ID}", userId);
                _logger.LogInformation ("Getting ExhibitId {ID}", model.ExhibitId);

                attendance = new Attendance
                {
                    AttendeeId = userId,
                    ExhibitId = model.ExhibitId
                };

                _repository.Add (attendance);
                _repository.SaveAll ();

                return Ok ();
            }
            catch (Exception ex)
            {
                _logger.LogError ($"Failed to add attendee: {ex}");
            }

            return BadRequest ("Failed to add attendee");
        }
    }
}
