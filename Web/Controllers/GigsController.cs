using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers {
    public class GigsController : Controller {
        private readonly ApplicationDbContext _context;

        public GigsController () {
            _context = new ApplicationDbContext ();
        }

        public IActionResult Create () {

            var viewModel = new GigFormViewModel();

            return View (viewModel);
        }

    }
}