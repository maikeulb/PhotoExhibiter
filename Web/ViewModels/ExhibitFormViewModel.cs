using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhotoExhibiter.Models;

namespace PhotoExhibiter.ViewModels
{
    public class ExhibitFormViewModel
    {
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

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }

    }
}
