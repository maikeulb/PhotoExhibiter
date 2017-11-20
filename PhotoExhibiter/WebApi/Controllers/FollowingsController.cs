using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Application.Commands;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Domain.Models;

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
        public async Task<IActionResult> Follow ([FromBody] Follow.Command command)
        {

            command.UserId = _userManager.GetUserId (User);

            var result = await _mediator.Send (command);

            return result ? (IActionResult)Ok () : (IActionResult)BadRequest(result.FailureReason);
        }
    }
}
