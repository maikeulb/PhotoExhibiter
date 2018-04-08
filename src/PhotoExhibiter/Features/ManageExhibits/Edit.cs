using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;
using PhotoExhibiter.Infrastructure;
using PhotoExhibiter.Infrastructure.Interfaces;

namespace PhotoExhibiter.Features.ManageExhibits
{
    public class Edit
    {
        public class Query : IRequest<Result<Command>>
        {
            public int Id { get; set; }
        }

        public class Command : IRequest<Result>
        {
            public int Id { get; set; }
            public string Date { get; set; }
            public string Genre { get; set; }
            public string Location { get; set; }
            public string Photographer { get; set; }
            public string ImageUrl { get; set; }
            public bool IsCanceled { get; set; }

            [Display (Name = "Date")]
            public DateTime DateTime { get; set; }
            public IEnumerable<Genre> Genres { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Result<Command>>
        {
            private readonly IExhibitRepository _repository;
            private readonly IUrlComposer _urlComposer;

            public QueryHandler (IExhibitRepository repository,
                IUrlComposer urlComposer)
            {
                _repository = repository;
                _urlComposer = urlComposer;
            }

            public Result<Command> Handle (Query message)
            {
                var exhibit = _repository.GetExhibit (message.Id);

                if (exhibit == null)
                    return Result.Fail<Command> ("Exhibit does not exit");

                var command = new Command
                {
                    Id = exhibit.Id,
                    Photographer = exhibit.Photographer.Name,
                    Location = exhibit.Location,
                    Date = exhibit.DateTime.ToString ("d MMM yyyy"),
                    DateTime = exhibit.DateTime,
                    IsCanceled = exhibit.IsCanceled,
                    ImageUrl = _urlComposer.ComposeImgUrl(exhibit.ImageUrl)
                };

                return Result.Ok (command);
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator ()
            {
                RuleFor (m => m.Location)
                    .NotNull ().WithMessage ("Name is required.")
                    .Length (1, 100).WithMessage ("Length must be between 1 and 100 characters");
                RuleFor (m => m.Date)
                    .NotNull ()
                    .SetValidator (new FutureDateValidator ());
            }
        }

        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly IExhibitRepository _repository;

            public CommandHandler (IExhibitRepository repository)
            {
                _repository = repository;
            }

            public Result Handle (Command message)
            {
                var exhibit = _repository.GetExhibit (message.Id);

                if (exhibit == null)
                    return Result.Fail<Command> ("Exhibit does not exit");

                exhibit.ManagerUpdate (
                    message.Location,
                    message.DateTime);

                if (message.IsCanceled == true)
                    exhibit.Cancel ();

                _repository.SaveAll ();

                return Result.Ok ();
            }
        }
    }
}
