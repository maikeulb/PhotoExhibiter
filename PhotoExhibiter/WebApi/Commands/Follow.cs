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

            public Handler(IFollowingRepository repository)
            {
                _repository = repository;
            }

            public void Handle(Command message)
            {

                var following = _repository.GetFollowing(message.UserId, message.FolloweeId);

                following = new Following
                {
                    FollowerId = message.UserId,
                    FolloweeId = message.FolloweeId
                };
                _repository.Add (following);
                _repository.SaveAll ();
            }
        }
    }
}


