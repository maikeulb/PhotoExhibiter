using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Data;
using PhotoExhibiter.Dtos;
using PhotoExhibiter.Models;

namespace PhotoExhibiter.Controllers.Api
{
    [Route ("api/[Controller]")]
    [Authorize]
    public class FollowingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AttendancesController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public FollowingsController (ApplicationDbContext context,
            ILogger<AttendancesController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult Follow ([FromBody] FollowingDto dto)
        {
            try
            {
                var userId = _userManager.GetUserId (User);

                if (_context.Followings.Any (f => f.FolloweeId == userId && f.FolloweeId == dto.FolloweeId))
                {
                    return BadRequest ("Following already exists.");
                }

                _logger.LogInformation ("Getting UserId {ID}", userId);
                _logger.LogInformation ("Getting FolloweeId {ID}", dto.FolloweeId);

                var following = new Following
                {
                    FollowerId = userId,
                    FolloweeId = dto.FolloweeId
                };

                _context.Followings.Add (following);
                _context.SaveChanges ();

                return Ok ();
            }
            catch (Exception ex)
            {
                _logger.LogError ($"Failed to add following: {ex}");
            }

            return BadRequest ("Failed to add following");
        }
    }
}