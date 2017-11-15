using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Infrastructure;

namespace PhotoExhibiter.Presentation.Apis
{
    [Route ("api/[Controller]")]
    [Authorize]
    public class ExhibitsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AttendancesController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public ExhibitsController (ApplicationDbContext context,
            ILogger<AttendancesController> logger,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IActionResult Cancel (int id)
        {
            try
            {
                var userId = _userManager.GetUserId (User);
                var exhibit = _unitOfWork.Exhibits.GetExhibitWithAttendees (id);

                if (exhibit == null || exhibit.IsCanceled)
                    return NotFound ();

                if (exhibit.PhotographerId != userId)
                    return Unauthorized ();

                _logger.LogInformation ("Getting UserId {ID}", userId);

                exhibit.Cancel ();

                _unitOfWork.Complete ();

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
