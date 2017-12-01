using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Apis.Attendances;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Apis.Exhibits
{
    [Route ("api/[Controller]")]
    [Authorize]
    public class ExhibitsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AttendancesController> _logger;
        private readonly IMediator _mediator;

        public ExhibitsController (
            UserManager<ApplicationUser> userManager,
            ILogger<AttendancesController> logger,
            IMediator mediator)
        {
            _userManager = userManager;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpDelete]
        public async Task<IActionResult> Cancel ([FromBody] Cancel.Command command)
        {
            command.UserId = _userManager.GetUserId (User);

            var result = await _mediator.Send (command);

            return Ok ();

            /* return result.IsSuccess ? */
            /* (IActionResult) Ok () : */
            /* (IActionResult) BadRequest (result.Error); */
        }
    }
}