namespace PhotoExhibiter.WebApi.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using PhotoExhibiter.Domain.Commands;
    using PhotoExhibiter.Domain.Models;
    using PhotoExhibiter.Domain.Interfaces;
    using PhotoExhibiter.Domain.Queries;
    using PhotoExhibiter.Infrastructure.Repositories;
    using PhotoExhibiter.WebUI.Controllers;
    using PhotoExhibiter.WebUI.ViewModels;

    public class Cancel 
    {
        public class Command : IRequest
        {
            public string UserId { get; set; }
            public int ExhibitId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IExhibitRepository _repository;

            public Handler(IExhibitRepository repository)
            {
                _repository = repository;
            }

            public void Handle(Command message)
            {
                var exhibit = _repository.GetExhibitWithAttendees (message.ExhibitId);
                exhibit.Cancel ();
                _repository.SaveAll ();
            }
        }
    }
}
