using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Apis.ManageUsers
{
    public class Cancel
    {
        public class Command : IRequest<Result>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly IApplicationUserRepository _repository;

            public Handler (IApplicationUserRepository repository)
            {
                _repository = repository;
            }

            public Result Handle (Command message)
            {
                var user = _repository.GetPhotographerWithExhibits (message.Id);
                if (user == null)
                  return Result.Fail<Command> ("Exhibit does not exist");
                if (user.IsSuspended)
                  return Result.Fail<Command> ("Exhibit is already suspended.");

                user.Suspend();
                _repository.SaveAll ();

                return Result.Ok ();
            }
        }
    }
}
