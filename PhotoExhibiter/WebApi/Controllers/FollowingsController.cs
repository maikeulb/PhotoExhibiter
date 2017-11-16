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
    public class FollowingsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AttendancesController> _logger;
        private readonly IFollowingRepository _repository;

        public FollowingsController (
            UserManager<ApplicationUser> userManager,
            ILogger<AttendancesController> logger,
            IFollowingRepository repository)
        {
            _logger = logger;
            _userManager = userManager;
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Follow ([FromBody] FollowingApiModel model)
        {
            try
            {
                var userId = _userManager.GetUserId (User);

                var following = _repository.GetFollowing (userId, model.FolloweeId);
                if (following != null)
                    return BadRequest ("Following already exists.");

                _logger.LogInformation ("Getting UserId {ID}", userId);
                _logger.LogInformation ("Getting FolloweeId {ID}", model.FolloweeId);

                following = new Following
                {
                    FollowerId = userId,
                    FolloweeId = model.FolloweeId
                };

                _repository.Add (following);
                _repository.SaveAll ();

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
