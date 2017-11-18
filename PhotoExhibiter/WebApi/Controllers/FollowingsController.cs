using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Domain.Models;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.WebApi.Commands;

namespace PhotoExhibiter.WebApi.Apis
{
    [Route ("api/[Controller]")]
    [Authorize]
    public class FollowingsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AttendancesController> _logger;
        private readonly IFollowingRepository _repository;
        private readonly IMediator _mediator;

        public FollowingsController (
            UserManager<ApplicationUser> userManager,
            ILogger<AttendancesController> logger,
            IFollowingRepository repository,
            IMediator mediator)
        {
            _logger = logger;
            _userManager = userManager;
            _repository = repository;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task <IActionResult> Follow ([FromBody] Follow.Command command)
        {
            try
            {
                // validation
                command.UserId = _userManager.GetUserId (User);
                var following = _repository.GetFollowing (command.UserId, command.FolloweeId);
                if (following != null)
                    return BadRequest ("Following already exists.");
                // validation

                await _mediator.Send(command);

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
