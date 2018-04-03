using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Apis.ManageExhibits
{
    public class Cancel
    {
        public class Command : IRequest<Result>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly IExhibitRepository _repository;

            public Handler (IExhibitRepository repository)
            {
                _repository = repository;
            }

            public Result Handle (Command message)
            {
                var exhibit = _repository.GetExhibitWithAttendees (message.Id);
                if (exhibit == null)
                  return Result.Fail<Command> ("Exhibit does not exist");
                if (exhibit.IsCanceled)
                  return Result.Fail<Command> ("Exhibit is cancelled.");

                exhibit.Cancel ();
                _repository.SaveAll ();

                return Result.Ok ();
            }
        }
    }
}
