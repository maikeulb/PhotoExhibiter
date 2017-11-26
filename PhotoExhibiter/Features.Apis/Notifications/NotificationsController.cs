using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Features.Apis.Attendances;
using PhotoExhibiter.Features.Apis.Notifications;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Features.Api.Notifications
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