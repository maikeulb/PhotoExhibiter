namespace PhotoExhibiter.WebApi.Commands
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

    public class Follow
    {
        public class Command : IRequest
        {
            public string UserId { get; set; }
            public string FolloweeId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IFollowingRepository _repository;
            private readonly IMapper _mapper;

            public Handler(IFollowingRepository repository,
                    IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public void Handle(Command message)
            {
                var following = _mapper.Map<Command, Following>(message);

                _repository.Add (following);
                _repository.SaveAll ();
            }
        }
    }
}


