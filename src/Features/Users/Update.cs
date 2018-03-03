using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Features;
using PhotoExhibiter.Infrastructure;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Features.Users
{
    public class Update
    {
        public class Command : IRequest<Result>
        {
            public string Id { get; set; }
            public string ImageUrl { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly IApplicationUserRepository _repository;
            private readonly ILogger _logger;

            public CommandHandler (IApplicationUserRepository repository,
            ILogger<CommandHandler> logger)
            {
                _repository = repository;
                _logger = logger;
            }

            public Result Handle (Command message)
            {
                var applicationUser = _repository.GetUserById (message.Id);

                if (applicationUser == null)
                    return Result.Fail<Command> ("User does not exit");

                var command = new Command
                {
                    ImageUrl = message.ImageUrl,
                };

                _logger.LogInformation("command in Update object *********{}", command.ImageUrl);
                _logger.LogInformation("command in Update object *********{}", message.ImageUrl);
                applicationUser.ImageUrl = command.ImageUrl;
                _repository.SaveAll ();

                return Result.Ok (command);
            }
        }
    }
}
