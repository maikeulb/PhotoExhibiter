using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.WebUI.ViewModels;
using PhotoExhibiter.WebUI.Controllers;
using PhotoExhibiter.Infrastructure.Repositories;
using System.Collections.Generic;

namespace PhotoExhibiter.Domain.Queries
{
    public class GetMyAttendingExhibitsQuery : IRequest<ExhibitsViewModel>
    {
        public string UserId  { get; set;}
        public bool ShowActions { get; set;}
    }

    public class GetMyAttendingExhibitsQueryHandler : IRequestHandler<GetMyAttendingExhibitsQuery, ExhibitsViewModel>
    {
        private readonly IMediator _mediator;
        private readonly IExhibitRepository _repository;

        public GetMyAttendingExhibitsQueryHandler(
            IMediator mediator,
            IExhibitRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public ExhibitsViewModel Handle(GetMyAttendingExhibitsQuery message)
        {

            var model = new ExhibitsViewModel
            {
                UpcomingExhibits =  _repository.GetExhibitsUserAttending (message.UserId),
                ShowActions = message.ShowActions,
                Heading = "Exhibits I'm Attending"
            };

            return model;
        }
     }
}
