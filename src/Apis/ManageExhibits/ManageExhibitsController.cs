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

namespace PhotoExhibiter.Apis.ManageExhibits
{
    [Route ("api/[Controller]")]
    public class ManageExhibitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManageExhibitsController (ApplicationDbContext context) => _context = context;

        public IActionResult GetExhibits (string query = null)
        {
            var exhibitsQuery = _context.Exhibits;

            var exhibitsDtos = exhibitsQuery
                .ToList();
            
            return Ok(exhibitsDtos);    
        }
    }
}
