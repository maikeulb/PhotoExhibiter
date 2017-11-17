namespace PhotoExhibiter.Domain.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PhotoExhibiter.Domain.Models;
    using PhotoExhibiter.Domain.Interfaces;
    using PhotoExhibiter.Infrastructure.Repositories;
    using PhotoExhibiter.WebUI.Controllers;
    using PhotoExhibiter.WebUI.ViewModels;

    public class Mine
    {
        public class Query : IRequest<IEnumerable<Exhibit>>
        {
            public string UserId { get; set; }
        }

        /* public class Model */
        /* { */
            /* public int Id { get; set; } */
            /* public ApplicationUser Photographer { get; set; } */
            /* public string PhotographerId { get; set; } */
            /* public DateTime DateTime { get; set; } */
            /* public string Location { get; set; } */
            /* public int GenreId { get; set; } */
            /* public Genre Genre { get; set; } */
            /* public ICollection<Attendance> Attendances { get; private set; } //Is this ever used? */
            /* public IEnumerable<Exhibit> UpcomingExhibits { get; set; } */
        /* } */

        public class Handler : IRequestHandler<Query, IEnumerable<Exhibit>>
        {
            private readonly IExhibitRepository _repository;

            public Handler (IExhibitRepository repository)
            {
                _repository = repository;
            }

            public IEnumerable<Exhibit> Handle (Query message)
            {
                var model = _repository.GetUpcomingExhibitsByPhotographer (message.UserId);

                return model;
            }
        }
    }
}
