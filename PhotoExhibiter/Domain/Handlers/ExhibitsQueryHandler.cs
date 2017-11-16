using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Presentation.ViewModels;
using PhotoExhibiter.Presentation.Controllers;
using PhotoExhibiter.Infrastructure.Repositories;

namespace PhotoExhibiter.Domain.Handlers
{
    public class ExhibitsQueryHandler : IRequestHandler<ExhibitsQuery, ExhibitsViewModel>
    {
        private readonly IMediator _mediatr;
        private readonly IExhibitRepository _repository;

        public ExhibitsQueryHandler(
            IMediator mediatr, 
            IExhibitRepository repository)
        {
            _mediatr = mediatr;
            _repository = repository;
        }

        public ExhibitsViewModel Handle(ExhibitsQuery message)
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

    public class ExhibitsQuery : IRequest<ExhibitsViewModel>
    {
        public string QueryId { get; set;}
        public string UserId  { get; set;}
        public bool ShowActions { get; set;}
    }
}
