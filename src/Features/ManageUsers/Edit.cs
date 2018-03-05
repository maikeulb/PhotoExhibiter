using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using PhotoExhibiter.Infrastructure;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Features.ManageUsers
{
    public class Edit
    {
        public class Query : IRequest<Result<Command>>
        {
            public string Id { get; set; }
        }

        public class Command : IRequest<Result>
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string ImageUrl { get; set; }
            public bool IsSuspended { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Result<Command>>
        {
            private readonly IApplicationUserRepository _repository;

            public QueryHandler (IApplicationUserRepository repository)
            {
                _repository = repository;
            }

            public Result<Command> Handle (Query message)
            {
                var user = _repository.GetPhotographer(message.Id);

                if (user == null)
                    return Result.Fail<Command> ("User does not exit");

                var command = new Command
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    ImageUrl = user.ImageUrl,
                    IsSuspended = user.IsSuspended
                };

                return Result.Ok (command);
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator ()
            {
                RuleFor (m => m.Name)
                    .NotNull ().WithMessage("Name is required.");
            }
        }

        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly IApplicationUserRepository _repository;

            public CommandHandler (IApplicationUserRepository repository)
            {
                _repository = repository;
            }

            public Result Handle (Command message)
            {
                var user = _repository.GetPhotographerWithExhibits(message.Id);

                if (user == null)
                    return Result.Fail<Command> ("User does not exit");

                user.Name = message.Name;
                user.ImageUrl = message.ImageUrl;
                user.IsSuspended = message.IsSuspended;

                if (message.IsSuspended == true)
                    user.Suspend();

                _repository.SaveAll ();

                return Result.Ok ();
            }
        }
    }
}
