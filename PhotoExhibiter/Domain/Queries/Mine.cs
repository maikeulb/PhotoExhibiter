namespace PhotoExhibiter.Domain.Queries
{
    using AutoMapper;
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

        public class Handler : IRequestHandler<Query, IEnumerable<Exhibit>>
        {
            private readonly IExhibitRepository _repository;

            public Handler (IExhibitRepository repository)
            {
                _repository = repository;
            }

            public IEnumerable<Exhibit> Handle (Query message)
            {
                var exhibits = _repository.GetUpcomingExhibitsByPhotographer (message.UserId);

                return exhibits ;
            }
        }
    }
}
