using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Apis.Attendances;
using PhotoExhibiter.Apis.Notifications;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Apis.Notifications
{
    [Route ("api/[Controller]")]
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;

        public NotificationsController (
            UserManager<ApplicationUser> userManager,
            IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<IEnumerable<Notifications.Dto>> Notifications (Notifications.Query query)
        {
            query.UserId = _userManager.GetUserId (User);

            var dTo = await _mediator.Send (query);

            return dTo;
        }

        [HttpPost]
        public async Task<IActionResult> Read (Read.Command command)
        {
            command.UserId = _userManager.GetUserId (User);

            var result = await _mediator.Send (command);

            return result.IsSuccess ?
                (IActionResult) Ok () :
                (IActionResult) BadRequest (result.Error);
        }
    }
}
