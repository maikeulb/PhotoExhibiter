using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Infrastructure;
using PhotoExhibiter.Presentation.ApiModels;

namespace PhotoExhibiter.Presentation.Apis
{
    [Route ("api/[Controller]")]
    [Authorize]
    public class AttendancesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AttendancesController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController (ApplicationDbContext context,
            ILogger<AttendancesController> logger,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Attend ([FromBody] AttendanceApiModel model)
        {
            try
            {
                var userId = _userManager.GetUserId (User);

                var attendance = _unitOfWork.Attendances.GetAttendance (model.ExhibitId, userId);
                if (attendance != null)
                    return BadRequest ("The attendance already exists.");

                _logger.LogInformation ("Getting UserId {ID}", userId);
                _logger.LogInformation ("Getting ExhibitId {ID}", model.ExhibitId);

                attendance = new Attendance
                {
                    AttendeeId = userId,
                    ExhibitId = model.ExhibitId
                };

                _unitOfWork.Attendances.Add (attendance);
                _unitOfWork.Complete ();

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
