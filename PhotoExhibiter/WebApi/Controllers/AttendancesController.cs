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
    public class AttendancesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AttendancesController> _logger;
        private readonly IAttendanceRepository _repository;
        private readonly IMediator _mediator;

        public AttendancesController (
            UserManager<ApplicationUser> userManager,
            ILogger<AttendancesController> logger,
            IAttendanceRepository repository,
            IMediator mediator)
        {
            _logger = logger;
            _userManager = userManager;
            _repository = repository;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Attend ([FromBody] Attend.Command command)
        {
            command.UserId = _userManager.GetUserId (User);

            var result = await _mediator.Send (command);

            return result ? (IActionResult)Ok () : (IActionResult)BadRequest(result.FailureReason);
        }
    }
}
