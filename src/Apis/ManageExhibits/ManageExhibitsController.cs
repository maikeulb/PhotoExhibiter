using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Data.Context;
using PhotoExhibiter.Apis.Attendances;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Apis.ManageExhibits
{
    [Route ("api/[Controller]")]
    public class ManageExhibitsController : Controller
    {
        private readonly IMediator _mediator;

        public ManageExhibitsController (IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles="Admin, DemoAdmin")]
        public async Task<IActionResult> GetExhibits (GetExhibits.Query query)
        {
            var model = await _mediator.Send (query);

            return Ok(model);    
        }

        [HttpDelete]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Cancel ([FromBody] Cancel.Command command)
        {
            var result = await _mediator.Send (command);

            return result.IsSuccess ?
            (IActionResult) Ok () :
            (IActionResult) BadRequest (result.Error);
        }
    }
}
