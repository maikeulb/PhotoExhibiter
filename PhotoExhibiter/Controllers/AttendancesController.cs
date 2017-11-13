using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PhotoExhibiter.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Models;
using PhotoExhibiter.Dtos;
using System;

namespace PhotoExhibiter.Controllers
{
    [Route("api/[Controller]")]
    [Authorize]
    public class AttendancesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AttendancesController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public AttendancesController(ApplicationDbContext context,
            ILogger<AttendancesController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult Attend([FromBody]AttendanceDto dto)
        {
          try
          {
            var userId = _userManager.GetUserId(User);

            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.ExhibitId == dto.ExhibitId))
                {
                  return BadRequest("The attendance already exists.");
                }

             _logger.LogInformation("Getting UserId {ID}", userId);
             _logger.LogInformation("Getting GidId {ID}", dto.ExhibitId);

            var attendance = new Attendance
            {
                AttendeeId = userId,
                ExhibitId = dto.ExhibitId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

           return Ok();
          }
          catch (Exception ex)
          {
            _logger.LogError($"Failed to add attendee: {ex}");
          }

          return BadRequest("Failed to add attendee");
        }
    }
}
