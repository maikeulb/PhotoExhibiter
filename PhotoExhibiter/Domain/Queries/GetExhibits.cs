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
    public class GetExhibitsQuery : IRequest<ExhibitsViewModel>
    {
        public string QueryId { get; set;}
        public string UserId  { get; set;}
        public bool ShowActions { get; set;}
    }

    public class GetExhibitsQueryHandler : IRequestHandler<GetExhibitsQuery, ExhibitsViewModel>
    {
        private readonly IMediator _mediator;
        private readonly IExhibitRepository _repository;

        public GetExhibitsQueryHandler(
            IMediator mediator, 
            IExhibitRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public ExhibitsViewModel Handle(GetExhibitsQuery message)
        {

            var model = new ExhibitsViewModel
            {
                UpcomingExhibits =  _repository.GetUpcomingExhibits (message.QueryId),
                ShowActions = message.ShowActions,
                Heading = "Upcoming Exhibits"
            };

            return model;
        }
     }
}
