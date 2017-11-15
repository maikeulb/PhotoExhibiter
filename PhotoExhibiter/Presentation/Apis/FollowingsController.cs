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
    public class FollowingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AttendancesController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController (ApplicationDbContext context,
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
        public IActionResult Follow ([FromBody] FollowingApiModel model)
        {
            try
            {
                var userId = _userManager.GetUserId (User);

                var following = _unitOfWork.Followings.GetFollowing (userId, model.FolloweeId);
                if (following != null)
                    return BadRequest ("Following already exists.");

                _logger.LogInformation ("Getting UserId {ID}", userId);
                _logger.LogInformation ("Getting FolloweeId {ID}", model.FolloweeId);

                following = new Following
                {
                    FollowerId = userId,
                    FolloweeId = model.FolloweeId
                };

                _unitOfWork.Followings.Add (following);
                _unitOfWork.Complete ();

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