using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using PhotoExhibiter.Domain.Models;
using PhotoExhibiter.WebUI.Controllers;
using PhotoExhibiter.WebUI.ViewModels.Filters;

namespace PhotoExhibiter.WebUI.ViewModels
{
    public class ExhibitFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public int Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string Heading { get; set; }

        /* public string Action */
        /* { */
        /*     get */
        /*     { */
        /*         Expression<Func<ExhibitsController, IActionResult>> update = */
        /*             (c => c.Update (this)); */

                /* Expression<Func<ExhibitsController, IActionResult>> create = */
                /*     (c => c.Create (this)); */

                /* var action = (Id != 0) ? update : create; */
                /* return (action.Body as MethodCallExpression).Method.Name; */
            /* } */
        /* } */

        public DateTime GetDateTime ()
        {
            return DateTime.Parse (string.Format ("{0} {1}", Date, Time));
        }
    }
}
