using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
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

namespace PhotoExhibiter.Apis.ManageUsers
{
    [Route ("api/[Controller]")]
    public class ManageUsersController : Controller
    {
        private readonly IApplicationUserRepository _repository;

        public ManageUsersController (IApplicationUserRepository repository) => _repository = repository;

        [Authorize(Roles="Admin, DemoAdmin")]
        public IActionResult GetPhotographers (string query = null)
        {
            var usersInDb = _repository.GetAllPhotographers(query);

            var usersDto = usersInDb.Select( u =>  new UserDto()
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                ImageUrl = u.ImageUrl,
                IsSuspended = u.IsSuspended
            });
            
            return Ok(usersDto);    
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public IActionResult EditPhotographer(string id, UserDto userDto)
        {
            var userInDb = _repository.GetPhotographer(id);

            if (userInDb == null)
                return NotFound();

            userInDb.Name = userDto.Name;
            userInDb.ImageUrl = userDto.ImageUrl;

            if (userDto.IsSuspended == true)
                userInDb.Suspend();

            _repository.SaveAll();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles="Admin")]
        public IActionResult Cancel([FromBody]CancelDto command)
        {
            var userInDb = _repository.GetPhotographerWithExhibits (command.Id);

            if (userInDb == null)
                return NotFound();

            userInDb.Suspend();
            _repository.SaveAll();

            return Ok();
        }
    }

    public class CancelDto
    {
        public string Id { get; set; }
    }

    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public bool IsSuspended { get; set; }
    }
}
