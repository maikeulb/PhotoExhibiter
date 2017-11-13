using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Data;
using PhotoExhibiter.Models;

namespace PhotoExhibiter.Controllers.Api
{
    [Route ("api/[Controller]")]
    [Authorize]
    public class ExhibitsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AttendancesController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExhibitsController (ApplicationDbContext context,
            ILogger<AttendancesController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpDelete]
        public IActionResult Cancel (int id)
        {
            try
            {
                var userId = _userManager.GetUserId (User);
                var exhibit = _context.Exhibits.Single (e => e.Id == id &&
                    e.PhotographerId == userId);

                if (exhibit.IsCanceled)
                {
                    return NotFound ();
                }

                _logger.LogInformation ("Getting UserId {ID}", userId);

                exhibit.IsCanceled = true;

                var notification = new Notification
                {
                    DateTime = DateTime.Now,
                    Exhibit = exhibit,
                    Type = NotificationType.ExhibitCanceled
                };

                var attendees = _context.Attendances
                    .Where(a => a.ExhibitId == exhibit.Id)
                    .Select(a => a.Attendee)
                    .ToList();

                foreach (var attendee in attendees)
                {
                    var userNotification = new UserNotification
                    {
                        User = attendee,
                        Notification = notification
                    };

                    _context.UserNotifications.Add(userNotification);
                }

                _context.SaveChanges ();

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
