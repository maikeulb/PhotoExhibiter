using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Features.Apis.Attendances
{
    [Route ("api/[Controller]")]
    [Authorize]
    public class AttendancesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AttendancesController> _logger;
        private readonly IMediator _mediator;

        public AttendancesController (
            UserManager<ApplicationUser> userManager,
            ILogger<AttendancesController> logger,
            IMediator mediator)
        {
            _userManager = userManager;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Attend ([FromBody] Attend.Command command)
        {
            command.UserId = _userManager.GetUserId (User);

            var result = await _mediator.Send (command);

            return result.IsSuccess ?
                (IActionResult) Ok () :
                (IActionResult) BadRequest (result.Error);
        }

        [HttpDelete]
        public async Task<IActionResult> Cancel (Cancel.Command command)
        {
            command.UserId = _userManager.GetUserId (User);

            var response = await _mediator.Send (command);

            return Ok (response);
        }
    }
}