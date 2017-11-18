namespace PhotoExhibiter.WebUI.Queries
{
    using AutoMapper;
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

    public class Followees
    {
        public class Query : IRequest<IEnumerable<ApplicationUser>>
        {
            public string UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<ApplicationUser>>
        {
            private readonly IApplicationUserRepository _repository;

            public Handler(IApplicationUserRepository repository)
            {
                _repository = repository;
            }

            public IEnumerable<ApplicationUser> Handle(Query message)
            {
                var photographers =  _repository.GetPhotographersFollowedBy (message.UserId);

                return photographers;
            }
        }
    }
}
