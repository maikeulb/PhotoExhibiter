namespace PhotoExhibiter.Domain.Queries
{
    using System.Diagnostics;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PhotoExhibiter.Domain.Models;
    using PhotoExhibiter.Domain.Interfaces;
    using PhotoExhibiter.WebUI.ViewModels;
    using PhotoExhibiter.WebUI.Controllers;
    using PhotoExhibiter.Infrastructure.Repositories;
    using System.Collections.Generic;

    public class Attending
    {
        public class Query : IRequest<Model>
        {
            public string UserId  { get; set;}
            public bool ShowActions  { get; set;}
        }

        public class Model
        {
            public IEnumerable<Exhibit> UpcomingExhibits{ get; set; }
            public bool ShowActions { get; set; }
            public string Heading { get; set; }
        }

        public class Handler : IRequestHandler<Query, Model>
        {
            private readonly IExhibitRepository _repository;

            public Handler(IExhibitRepository repository)
            {
                _repository = repository;
            }

            public Model Handle(Query message)
            {
                var model = new Model
                {
                    UpcomingExhibits =  _repository.GetExhibitsUserAttending (message.UserId),
                    ShowActions = message.ShowActions,
                    Heading = "Exhibits I'm Attending"
                };

                return model;
            }
        }
    }
}
