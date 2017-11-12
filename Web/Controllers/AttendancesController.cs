
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Web.Models;
using Web.DTOs;
using System;

namespace Web.Controllers
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
        public IActionResult Post([FromBody]AttendanceDTO DTO)
        {
          try
          {
            var userId = _userManager.GetUserId(User);
        
            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == DTO.GigId))
                return BadRequest("The attendance already exists.");

             _logger.LogInformation("Getting user item {ID}", userId);
             _logger.LogInformation("*******************");
             _logger.LogInformation("Getting gid item {ID}", DTO.GigId);

            var attendance = new Attendance
            {
                GigId = DTO.GigId,
                AttendeeId = userId
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
