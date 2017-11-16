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
    public class GetMyExhibitsQuery : IRequest<IEnumerable<Exhibit>>
    {
        public string UserId  { get; set;}
    }

    public class GetMyExhibitsQueryHandler : IRequestHandler<GetMyExhibitsQuery, IEnumerable<Exhibit>>
    {
        private readonly IMediator _mediator;
        private readonly IExhibitRepository _repository;

        public GetMyExhibitsQueryHandler(
            IMediator mediator,
            IExhibitRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public IEnumerable<Exhibit> Handle(GetMyExhibitsQuery message)
        {
            var model = _repository.GetUpcomingExhibitsByPhotographer (message.UserId);

            return model;
        }
     }
}
